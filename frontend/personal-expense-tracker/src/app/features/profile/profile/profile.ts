
import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Auth, ProfileResponse } from '../../../services/auth';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  private fb = inject(FormBuilder);
  private authService = inject(Auth);
private cdr = inject(ChangeDetectorRef);
  profile: ProfileResponse | null = null;

  isEditing = false;
  isLoading = false;

  successMessage = '';
  errorMessage = '';

  profileForm = this.fb.group({
    fullName: ['', [Validators.required, Validators.minLength(2)]],
    phoneNumber: ['']
  });

  ngOnInit(): void {
    this.loadProfile();
  }

 loadProfile(): void {
  this.isLoading = true;
  this.errorMessage = '';

  console.log('Loading profile started');

  this.authService.getProfile().subscribe({
    next: (response) => {
      console.log('Profile API response:', response);

      this.profile = response;

      this.profileForm.patchValue({
        fullName: response.fullName,
        phoneNumber: response.phoneNumber || ''
      });

      this.isLoading = false;

      console.log('Loading completed:', this.isLoading);

      this.cdr.markForCheck();
    },
    error: (error) => {
      console.error('Profile API error:', error);

      this.errorMessage =
        error.error?.message || 'Unable to load profile. Please login again.';

      this.isLoading = false;
      this.cdr.markForCheck();
    }
  });
}
  enableEdit(): void {
    if (!this.profile) {
      return;
    }

    this.profileForm.patchValue({
      fullName: this.profile.fullName,
      phoneNumber: this.profile.phoneNumber || ''
    });

    this.isEditing = true;
    this.successMessage = '';
    this.errorMessage = '';
  }

  cancelEdit(): void {
    this.isEditing = false;

    this.profileForm.patchValue({
      fullName: this.profile?.fullName || '',
      phoneNumber: this.profile?.phoneNumber || ''
    });

    this.successMessage = '';
    this.errorMessage = '';
  }

  updateProfile(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.profileForm.invalid) {
      this.profileForm.markAllAsTouched();
      return;
    }

    const { fullName, phoneNumber } = this.profileForm.getRawValue();

    this.isLoading = true;

    this.authService.updateProfile({
      fullName: fullName!,
      phoneNumber: phoneNumber || null
    }).subscribe({
      next: (response) => {
        this.profile = response.profile;
        this.isEditing = false;
        this.successMessage = response.message;
        this.isLoading = false;
          this.cdr.markForCheck();
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Unable to update profile.';
  this.cdr.markForCheck();
        this.isLoading = false;
      }
    });
  }
}
