import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private uploadUrl = 'https://api.imgbb.com/1/upload';
  private apiKey = '8c8ce81a714d22cb8e6e71c2dd4dd49d';
  private usersApi = 'https://localhost:7147/api/users/GetUsers';

  constructor(private http: HttpClient) { }

  // دالة رفع الصورة إلى Imgbb
  uploadImage(imageData: File): Observable<any> {
    const formData = new FormData();
    formData.append('image', imageData);
    formData.append('key', this.apiKey);
    return this.http.post(this.uploadUrl, formData);
  }

  // دالة جلب جميع المستخدمين
  getAllUsers(): Observable<any> {
    return this.http.get(this.usersApi);
  }
}
