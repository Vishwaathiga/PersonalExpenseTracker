
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RegisterRequest {
  fullName: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private http = inject(HttpClient);

 private apiUrl = 'https://localhost:7082/api/auth';

  register(request: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${this.apiUrl}/register`,
      request
    );
  }
}
