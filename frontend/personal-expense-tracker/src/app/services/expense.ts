import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Expense {
  id: number;
  title: string;
  amount: number;
  description: string | null;
  expenseDate: string;
  categoryId: number | null;
  categoryName: string | null;
}

export interface CreateExpenseRequest {
  title: string;
  amount: number;
  description: string | null;
  expenseDate: string;
  categoryId: number | null;
}

export interface UpdateExpenseRequest {
  title: string;
  amount: number;
  description: string | null;
  expenseDate: string;
  categoryId: number | null;
}

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7082/api/expense';

 private getAuthHeaders(): HttpHeaders {
  const token = localStorage.getItem('token');

  return new HttpHeaders({
    Authorization: `Bearer ${token}`
  });
}

  getAllExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>(
      this.apiUrl,
      { headers: this.getAuthHeaders() }
    );
  }

  getExpenseById(id: number): Observable<Expense> {
    return this.http.get<Expense>(
      `${this.apiUrl}/${id}`,
      { headers: this.getAuthHeaders() }
    );
  }

  createExpense(request: CreateExpenseRequest): Observable<Expense> {
    return this.http.post<Expense>(
      this.apiUrl,
      request,
      { headers: this.getAuthHeaders() }
    );
  }

  updateExpense(
    id: number,
    request: UpdateExpenseRequest
  ): Observable<any> {
    return this.http.put<any>(
      `${this.apiUrl}/${id}`,
      request,
      { headers: this.getAuthHeaders() }
    );
  }

  deleteExpense(id: number): Observable<any> {
    return this.http.delete<any>(
      `${this.apiUrl}/${id}`,
      { headers: this.getAuthHeaders() }
    );
  }
}