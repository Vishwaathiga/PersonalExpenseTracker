
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CategoryReport {
  categoryName: string;
  totalAmount: number;
  transactionCount: number;
}

export interface ReportExpense {
  id: number;
  title: string;
  amount: number;
  categoryName: string | null;
  expenseDate: string;
}

export interface MonthlyReport {
  year: number;
  month: number;
  totalExpenses: number;
  totalTransactions: number;
  categoryBreakdown: CategoryReport[];
  expenses: ReportExpense[];
}

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  private apiUrl = 'https://localhost:7082/api/reports';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getMonthlyReport(year: number, month: number): Observable<MonthlyReport> {
    return this.http.get<MonthlyReport>(
      `${this.apiUrl}/monthly?year=${year}&month=${month}`,
      { headers: this.getHeaders() }
    );
  }
}
