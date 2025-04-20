import { Component } from '@angular/core';
import { Booking, BookingService } from '../service/booking.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrl: './booking.component.css'
})
export class BookingComponent {
  bookings: Booking[] = [];

  constructor(private bookingService: BookingService) { }

  ngOnInit() {
    this.loadBookings();
  }

  loadBookings() {
    this.bookingService.getAllBookings().subscribe(data => {
      this.bookings = data;
    });
  }

  confirm(id: number) {
    this.bookingService.confirmBooking(id).subscribe({
      next: () => {
        alert('Booking confirmed!');
        this.loadBookings();
      },
      error: () => {
        alert('Failed to confirm booking.');
      }
    });
  }
}
