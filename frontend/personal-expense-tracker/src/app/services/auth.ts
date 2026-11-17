
import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  message: string;
}

export interface LoginResponse {
  token: string;
  expiresAt: string;
  fullName: string;
  email: string;
}

export interface ProfileResponse {
  id: string;
  fullName: string;
  email: string;
  phoneNumber: string | null;
}

export interface UpdateProfileRequest {
  fullName: string;
  phoneNumber: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7082/api/auth';
  private profileUrl = 'https://localhost:7082/api/profile';

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.apiUrl}/register`,
      request
    );
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/login`,
      request
    );
  }

  getProfile(): Observable<ProfileResponse> {
    return this.http.get<ProfileResponse>(
      this.profileUrl,
      { headers: this.getAuthHeaders() }
    );
  }

  updateProfile(request: UpdateProfileRequest): Observable<any> {
    return this.http.put<any>(
      this.profileUrl,
      request,
      { headers: this.getAuthHeaders() }
    );
  }

  private getAuthHeaders(): HttpHeaders {
    const token = sessionStorage.getItem('token');

    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }
}
