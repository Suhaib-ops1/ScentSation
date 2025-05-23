import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ContactMessageService {

  constructor(private http: HttpClient) { }

  getAllMessages(): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:7199/api/Admin/GetAllContactMessages`);
  }
}
