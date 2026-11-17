
import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReportsService,
  MonthlyReport
} from '../../../services/reports';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reports.html',
  styleUrl: './reports.css'
})
export class ReportsComponent implements OnInit {

  private reportsService = inject(ReportsService);
  private cdr = inject(ChangeDetectorRef);

  report: MonthlyReport | null = null;

  selectedMonth = this.getCurrentMonth();

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadReport();
  }

  private getCurrentMonth(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');

    return `${year}-${month}`;
  }

  onMonthChange(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (!input.value) {
      return;
    }

    this.selectedMonth = input.value;
    this.loadReport();
  }

  loadReport(): void {
    const [year, month] = this.selectedMonth.split('-').map(Number);

    this.isLoading = true;
    this.errorMessage = '';
    this.report = null;

    this.reportsService.getMonthlyReport(year, month).subscribe({
      next: (response) => {
        this.report = response;
        this.isLoading = false;
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Unable to load monthly report.';

        this.isLoading = false;
        this.cdr.markForCheck();
      }
    });
  }

  getCategoryPercentage(amount: number): number {
    if (!this.report || this.report.totalExpenses === 0) {
      return 0;
    }

    return (amount / this.report.totalExpenses) * 100;
  }
}
