
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { RouterLink } from '@angular/router';

import { Auth } from '../../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private fb = inject(FormBuilder);
  private authService = inject(Auth);

  successMessage = '';
  errorMessage = '';
  isLoading = false;

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const { email, password } = this.loginForm.getRawValue();

    this.isLoading = true;

    this.authService.login({
      email: email!,
      password: password!
    }).subscribe({
      next: (response) => {
        sessionStorage.setItem('token', response.token);
        sessionStorage.setItem('fullName', response.fullName);
        sessionStorage.setItem('email', response.email);

        this.successMessage = `Welcome, ${response.fullName}! Login successful.`;
        this.loginForm.reset();
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Login failed. Please try again.';

        this.isLoading = false;
      }
    });
  }
}
