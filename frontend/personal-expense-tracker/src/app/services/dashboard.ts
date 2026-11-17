
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CategorySpending {
  categoryName: string;
  totalAmount: number;
}

export interface RecentExpense {
  id: number;
  title: string;
  amount: number;
  categoryName: string | null;
  expenseDate: string;
}

export interface DashboardResponse {
  totalExpenses: number;
  todayExpenses: number;
  monthlyExpenses: number;
  totalTransactions: number;
  categorySpending: CategorySpending[];
  recentExpenses: RecentExpense[];
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private apiUrl = 'https://localhost:7082/api/dashboard';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getDashboard(): Observable<DashboardResponse> {
    return this.http.get<DashboardResponse>(
      this.apiUrl,
      { headers: this.getHeaders() }
    );
  }
}
