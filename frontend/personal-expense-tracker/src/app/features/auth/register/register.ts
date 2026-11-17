
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Auth } from '../../../services/auth';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  private fb = inject(FormBuilder);
  private authService = inject(Auth);

  successMessage = '';
  errorMessage = '';
  isLoading = false;

  registerForm = this.fb.group({
    fullName: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  });

  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const { fullName, email, password, confirmPassword } =
      this.registerForm.getRawValue();

    if (password !== confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    this.isLoading = true;

    this.authService.register({
      fullName: fullName!,
      email: email!,
      password: password!
    }).subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.registerForm.reset();
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Registration failed. Please try again.';
        this.isLoading = false;
      }
    });
  }
}
