import { Component, OnInit } from '@angular/core';
import { S1Service } from '../service/s1.service';

@Component({
  selector: 'app-showusers',
  templateUrl: './showusers.component.html',
  styleUrl: './showusers.component.css'
})
export class ShowusersComponent implements OnInit {
  prod: any[] = [];

  constructor(private _pp: S1Service) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this._pp.getallusers().subscribe({
      next: (data) => {
        console.log('✅ Users received:', data);
        this.prod = data;
      },
      error: (err) => {
        console.error('❌ Failed to load users:', err);
      }
    });
  }
}
