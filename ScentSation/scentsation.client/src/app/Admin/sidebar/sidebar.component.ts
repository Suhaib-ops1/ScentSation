import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {


  constructor(private router: Router) { }

  logout(): void {
    // Clear session/local storage
    localStorage.clear();
    sessionStorage.clear();

    // Navigate to login (or home)
    this.router.navigate(['/login']);
  }
}
