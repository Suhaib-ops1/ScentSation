import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserlService } from '../userl.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  loggedInRole$ = new BehaviorSubject<string | null>(null);

  constructor(
    private fb: FormBuilder,
    private userService: UserlService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Please fill all fields correctly.';
      return;
    }

    this.userService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        this.successMessage = res.message;
        this.errorMessage = '';

        // 👇 تخزين بالسشن والبيهيفير
        sessionStorage.setItem('userId', res.userId);
        sessionStorage.setItem('role', res.role);
        this.loggedInRole$.next(res.role);

        // 👇 توجيه حسب الدور
        switch (res.role) {
          case 'Admin':
            this.router.navigate(['/admin-dashboard']);
            break;
          case 'Staff':
            this.router.navigate(['/staff-dashboard']);
            break;
          default:
            this.router.navigate(['/']);
        }
      },
      error: err => {
        this.errorMessage = err.error || 'Login failed';
        this.successMessage = '';
      }
    });
  }
}
