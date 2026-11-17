
import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  DashboardService,
  DashboardResponse
} from '../../../services/dashboard';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class DashboardComponent implements OnInit {

  private dashboardService = inject(DashboardService);
  private cdr = inject(ChangeDetectorRef);

  dashboard: DashboardResponse | null = null;

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.dashboardService.getDashboard().subscribe({
      next: (response) => {
        this.dashboard = response;
        this.isLoading = false;
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Unable to load dashboard.';

        this.isLoading = false;
        this.cdr.markForCheck();
      }
    });
  }

  getCategoryPercentage(amount: number): number {
    if (!this.dashboard || this.dashboard.totalExpenses === 0) {
      return 0;
    }

    return (amount / this.dashboard.totalExpenses) * 100;
  }
}
