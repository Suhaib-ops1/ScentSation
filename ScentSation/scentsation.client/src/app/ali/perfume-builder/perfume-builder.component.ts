import { Component, OnInit } from '@angular/core';
import { PerfumeBuilderService } from '../perfume-builder.service';
import {
  trigger,
  transition,
  style,
  animate,
  keyframes
} from '@angular/animations';
import { OrderCartService } from '../../suhaib/order-cart.service';

@Component({
  selector: 'app-perfume-builder',
  templateUrl: './perfume-builder.component.html',
  styleUrls: ['./perfume-builder.component.css'],
  animations: [
    trigger('fadeInContainer', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('600ms ease-out', style({ opacity: 1 }))
      ])
    ]),
    trigger('fadeSlide', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate('500ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ]),
    trigger('pulseRepeat', [
      transition(':enter', [
        animate('1500ms ease-in-out', keyframes([
          style({ transform: 'scale(1)', offset: 0 }),
          style({ transform: 'scale(1.05)', offset: 0.5 }),
          style({ transform: 'scale(1)', offset: 1 })
        ]))
      ])
    ])

  ]
})
export class PerfumeBuilderComponent implements OnInit {

  userId: number = 1;
  perfumeName: string = '';
  perfumePrice: number = 0;

  allNotes: any[] = [];
  topNotes: any[] = [];
  middleNotes: any[] = [];
  baseNotes: any[] = [];
  selectedNoteIds: number[] = [];

  bottles: any[] = [];
  selectedBottle: any = null;

  message: string = '';

  constructor(
    private perfumeService: PerfumeBuilderService,
    private cartService: OrderCartService 
  ) { }

  ngOnInit(): void {
    const sessionId = sessionStorage.getItem('userId');
    if (sessionId) this.userId = parseInt(sessionId);

    this.perfumeService.getAllNotes().subscribe((notes: any[]) => {
      this.allNotes = notes;
      this.topNotes = notes.filter(n => n.type === 'Top');
      this.middleNotes = notes.filter(n => n.type === 'Middle');
      this.baseNotes = notes.filter(n => n.type === 'Base');
    });

    this.perfumeService.getAllBottles().subscribe(bottles => {
      this.bottles = bottles;
    });
  }

  toggleNote(noteId: number) {
    if (this.selectedNoteIds.includes(noteId)) {
      this.selectedNoteIds = this.selectedNoteIds.filter(id => id !== noteId);
    } else {
      this.selectedNoteIds.push(noteId);
    }
    this.calculatePrice();
  }

  selectBottle(bottle: any) {
    this.selectedBottle = bottle;
    this.calculatePrice();
  }

  calculatePrice() {
    const notePrice = 2.5;
    const bottlePrice = this.selectedBottle?.price || 0;
    this.perfumePrice = bottlePrice + (this.selectedNoteIds.length * notePrice);
  }

  confirmPerfume() {
    if (!this.perfumeName || !this.selectedBottle || this.selectedNoteIds.length === 0) {
      this.message = '❌ Please complete all steps.';
      return;
    }

    const perfumeData = {
      userId: this.userId,
      name: this.perfumeName,
      bottleId: this.selectedBottle.bottleId,
      price: this.perfumePrice,
      customPerfumeNotes: this.selectedNoteIds.map(noteId => ({ noteId }))
    };

    this.perfumeService.createCustomPerfume(perfumeData).subscribe((createdPerfume: any) => {
      const cartItem = {
        userId: this.userId,
        customPerfumeId: createdPerfume.customPerfumeId,
        quantity: 1
      };

      this.cartService.addToCart(cartItem).subscribe(() => {
        this.message = '✅ Your perfume has been added to the cart!';
        this.resetForm();
      });
    });
  }

  resetForm() {
    this.perfumeName = '';
    this.perfumePrice = 0;
    this.selectedBottle = null;
    this.selectedNoteIds = [];
  }

  isNoteSelected(id: number) {
    return this.selectedNoteIds.includes(id);
  }
}
