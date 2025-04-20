import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserlService } from '../userl.service';

interface RegisterResponse {
  message: string;
  userId: number;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm!: FormGroup;
  message: string = '';

  constructor(private fb: FormBuilder, private userService: UserlService) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$/)  // حروف وأرقام
      ]],
      confirmPassword: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  submit(): void {
    if (this.registerForm.invalid) {
      this.message = '❌ Please fill all required fields correctly.';
      return;
    }

    const formData = {
      fullName: this.registerForm.value.fullName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      phoneNumber: this.registerForm.value.phoneNumber
    };

    this.userService.register(formData).subscribe({
      next: (res: RegisterResponse) => {
        this.message = res.message;
        this.registerForm.reset();
      },
      error: (err: any) => {
        this.message = '❌ Registration failed.';
      }
    });
  }
}
