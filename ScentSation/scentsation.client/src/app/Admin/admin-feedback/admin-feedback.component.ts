import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-feedback',
  templateUrl: './admin-feedback.component.html',
  styleUrl: './admin-feedback.component.css'
})
export class AdminFeedbackComponent implements OnInit {
  messages: any[] = [];
  isLoading = true;
  hasError = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<any[]>('https://localhost:7199/api/Admin/GetAllContactMessages')
      .subscribe({
        next: (data) => {
          console.log('Messages from API:', data);
          this.messages = data;
          this.isLoading = false;
        },
        error: (err) => {
          console.error('Error fetching messages:', err);
          this.hasError = true;
          this.isLoading = false;
        }
      });
  }
}
