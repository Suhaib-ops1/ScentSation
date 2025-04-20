
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MyService {

  constructor(private _http: HttpClient) { }

  getAllAvailableSessions() {

    return this._http.get('https://localhost:7199/api/User/AvailableSessions');

  }
  getAllBookedSessions() {
    return this._http.get('https://localhost:7199/api/User/BookedSessions');

  }
  AppointmentsById(id: any) {
    return this._http.get(`https://localhost:7199/api/User/getMyAppointments/${id}`);

  }
  ReserveSession(id: any, session: any) {
    return this._http.put(`https://localhost:7199/api/User/ReserveSession/${id}`, session);
  }
  getAllStaffAppointments(id: any) {
    return this._http.get(`https://localhost:7199/api/User/CancelSession/${id}`);
  }
}
