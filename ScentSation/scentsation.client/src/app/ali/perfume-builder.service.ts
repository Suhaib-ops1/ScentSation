import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PerfumeBuilderService {

  private perfumeUrl = 'https://localhost:7199/api/customperfume';
  private notesUrl = 'https://localhost:7199/api/perfumenote';
  private bottlesUrl = 'https://localhost:7199/api/bottle';

  constructor(private http: HttpClient) { }

  // ✅ جلب كل النوتات (Top / Middle / Base)
  getAllNotes(): Observable<any[]> {
    return this.http.get<any[]>(this.notesUrl);
  }

  // ✅ جلب كل الزجاجات المتاحة
  getAllBottles(): Observable<any[]> {
    return this.http.get<any[]>(this.bottlesUrl);
  }

  // ✅ إنشاء عطر مخصص
  createCustomPerfume(data: any): Observable<any> {
    return this.http.post<any>(this.perfumeUrl, data);
  }

  // ✅ جلب كل العطور المصممة لمستخدم معيّن
  getPerfumesByUser(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.perfumeUrl}/user/${userId}`);
  }
}
