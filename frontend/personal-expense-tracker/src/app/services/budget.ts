
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Budget {
  id: number;
  amount: number;
  year: number;
  month: number;
  totalSpent: number;
  remainingAmount: number;
  percentageUsed: number;
  isExceeded: boolean;
}

export interface CreateBudgetRequest {
  amount: number;
  year: number;
  month: number;
}

export interface UpdateBudgetRequest {
  amount: number;
}

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  private apiUrl = 'https://localhost:7082/api/budget';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getBudget(year: number, month: number): Observable<Budget> {
    return this.http.get<Budget>(
      `${this.apiUrl}?year=${year}&month=${month}`,
      { headers: this.getHeaders() }
    );
  }

  createBudget(request: CreateBudgetRequest): Observable<Budget> {
    return this.http.post<Budget>(
      this.apiUrl,
      request,
      { headers: this.getHeaders() }
    );
  }

  updateBudget(id: number, request: UpdateBudgetRequest): Observable<Budget> {
    return this.http.put<Budget>(
      `${this.apiUrl}/${id}`,
      request,
      { headers: this.getHeaders() }
    );
  }
}
