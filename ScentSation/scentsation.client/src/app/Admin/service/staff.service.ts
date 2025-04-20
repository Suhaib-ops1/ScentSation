import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface StaffDTO {
  fullName: string;
  email: string;
  phoneNumber: string;
  profileImageUrl: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  constructor(private http: HttpClient) { }

  getAllStaff(): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:7199/api/Staff/GetAllStaff`);
}

  addStaff(staff: StaffDTO): Observable<boolean> {
    return this.http.post<boolean>(`https://localhost:7199/api/Staff/AddStaff`, staff);
}
}
