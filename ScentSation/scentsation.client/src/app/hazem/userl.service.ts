import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserlService {

  private apiUrl = 'https://localhost:7199/api/Users';

  constructor(private http: HttpClient) { }

  register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  login(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, data);
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/logout`, {});
  }

  forgotPassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, data);
  }

  resetPassword(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, data);
  }

  getProfile(): Observable<any> {
    return this.http.get(`${this.apiUrl}/profile`);
  }

  // ✅ دالة جلب جميع المستخدمين
  getAllUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetUsers`);
  }
  updateUser(id: string, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/UpdateUser/${id}`, data);
  }
  getUserById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/GetUserById/${id}`);
  }

}
