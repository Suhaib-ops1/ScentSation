import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-fragrance-ai',
  templateUrl: './fragrance-ai.component.html',
  styleUrls: ['./fragrance-ai.component.css']
})
export class FragranceAiComponent implements OnInit {
  gender: string = '';
  questions: any[] = [];
  answers: string[] = [];
  isLoading = false;
  stage: 'gender' | 'questions' | 'result' = 'gender';

  bottlesFromDb: any[] = [];
  notesFromDb: any[] = [];

  resultPerfume: any = null;
  selectedBottle: any = null;
  selectedNotes: any[] = [];

  userId: number = 1;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    const session = sessionStorage.getItem('userId');
    if (session) this.userId = parseInt(session);
    this.loadPerfumeData();
  }

  loadPerfumeData() {
    this.http.get<any[]>('/api/bottles').subscribe(data => this.bottlesFromDb = data);
    this.http.get<any[]>('/api/notes').subscribe(data => this.notesFromDb = data);
  }

  selectGender(selected: string) {
    this.gender = selected;
    this.stage = 'questions';
    this.generateQuestions();
  }

  generateQuestions() {
    this.isLoading = true;
    const prompt = `
You are an AI assistant for choosing perfumes. Generate 7 multiple-choice questions (4 options each) tailored for a ${this.gender}.
Respond ONLY with valid JSON:
[
  {
    "question": "Your ideal weekend activity?",
    "options": ["Spa", "Hiking", "Reading", "Partying"]
  }
]`;

    const body = { contents: [{ parts: [{ text: prompt }] }] };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    this.http.post('https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyDhldS_zrE-Hrpr2D4gestolelOiTdFKzA', body, { headers }).subscribe({
      next: (res: any) => {
        try {
          const raw = res?.candidates?.[0]?.content?.parts?.[0]?.text || '';
          const start = raw.indexOf('[');
          const end = raw.lastIndexOf(']') + 1;
          this.questions = JSON.parse(raw.substring(start, end));
        } catch {
          alert('❌ Failed to parse questions.');
        }
        this.isLoading = false;
      },
      error: err => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }

  selectOption(index: number, option: string) {
    this.answers[index] = option;
  }

  submitAnswers() {
    this.isLoading = true;

    const bottleText = this.bottlesFromDb.map(b => `${b.bottleId}:${b.name} - ${b.price} JOD`).join(', ');
    const noteText = this.notesFromDb.map(n => `${n.noteId}:${n.name}`).join(', ');

    const prompt = `
Based on user's gender (${this.gender}) and answers: ${JSON.stringify(this.answers)},
select one bottle ONLY from: ${bottleText},
and 3-4 notes ONLY from: ${noteText}.
Price = bottle price + 2.5 per note.
Return valid JSON only:
{
  "name": "My Perfume",
  "price": 30.5,
  "bottleId": 1,
  "noteIds": [2, 4, 5],
  "description": "This is your unique scent."
}`;

    const body = { contents: [{ parts: [{ text: prompt }] }] };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    this.http.post('https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyDhldS_zrE-Hrpr2D4gestolelOiTdFKzA', body, { headers }).subscribe({
      next: (res: any) => {
        try {
          const raw = res?.candidates?.[0]?.content?.parts?.[0]?.text || '';
          const clean = raw.replace(/```json|```/g, '').trim();
          const aiResult = JSON.parse(clean);

          const perfumePayload = {
            userId: this.userId,
            name: aiResult.name,
            bottleId: aiResult.bottleId,
            price: aiResult.price,
            customPerfumeNoteIds: aiResult.noteIds
          };

          // ✅ حفظ العطر وإضافته للسلة مباشرة
          this.http.post('https://localhost:7199/api/ai/save-ai-perfume', perfumePayload).subscribe((created: any) => {
            const cartItem = {
              userId: this.userId,
              customPerfumeId: created.customPerfumeId,
              quantity: 1
            };

            this.http.post('https://localhost:7199/api/cart', cartItem).subscribe(() => {
              this.resultPerfume = { ...aiResult, customPerfumeId: created.customPerfumeId };
              this.selectedBottle = this.bottlesFromDb.find(b => b.bottleId === aiResult.bottleId);
              this.selectedNotes = this.notesFromDb.filter(n => aiResult.noteIds.includes(n.noteId));
              this.stage = 'result';
              this.isLoading = false;
            });
          });

        } catch (err) {
          console.error('❌ Parse AI JSON Error:', err);
          this.isLoading = false;
        }
      },
      error: err => {
        console.error('❌ AI Error:', err);
        this.isLoading = false;
      }
    });
  }

  getNoteNames(): string {
    return this.selectedNotes.map(n => n.name).join(', ');
  }

  goToCart() {
    this.router.navigate(['/cart']);
  }

}
