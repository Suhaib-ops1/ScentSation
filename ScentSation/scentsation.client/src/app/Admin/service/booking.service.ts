import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Booking {
  appointmentId: number;
  userId: number;
  staffId: number;
  appointmentDate: string;
  timeSlot: string;
  status: string;
  notes?: string;
  createdAt?: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http: HttpClient) { }

  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`https://localhost:7199/api/Booking/GetAllBookings`);
}

  confirmBooking(id: number): Observable<void> {
    return this.http.put<void>(`https://localhost:7199/api/Booking/ConfirmBooking/${id}`, {});
}
}
