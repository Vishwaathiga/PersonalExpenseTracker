
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  BudgetService,
  Budget
} from '../../../services/budget';

@Component({
  selector: 'app-budget',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './budget.html',
  styleUrl: './budget.css'
})
export class BudgetComponent implements OnInit {

  budget: Budget | null = null;

  selectedMonth = this.getCurrentMonth();

  isEditing = false;
  isLoading = false;

  errorMessage = '';
  successMessage = '';

  budgetForm: any;

  constructor(
    private fb: FormBuilder,
    private budgetService: BudgetService,
    private cdr: ChangeDetectorRef
  ) {
    this.budgetForm = this.fb.group({
      amount: ['', [
        Validators.required,
        Validators.min(0.01)
      ]]
    });
  }

  ngOnInit(): void {
    this.loadBudget();
  }

  getCurrentMonth(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');

    return `${year}-${month}`;
  }

  onMonthChange(): void {
    this.isEditing = false;
    this.budget = null;
    this.budgetForm.reset();
    this.loadBudget();
  }

  loadBudget(): void {
    if (!this.selectedMonth) {
      return;
    }

    const [year, month] = this.selectedMonth.split('-').map(Number);

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.budgetService.getBudget(year, month).subscribe({
      next: (response) => {
        this.budget = response;
        this.budgetForm.patchValue({
          amount: response.amount
        });
        this.isEditing = false;
        this.isLoading = false;
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.budget = null;
        this.isEditing = false;
        this.isLoading = false;

        if (error.status === 404) {
          this.errorMessage = '';
        } else {
          this.errorMessage =
            error.error?.message || 'Unable to load budget.';
        }

        this.cdr.markForCheck();
      }
    });
  }

  saveBudget(): void {
    if (this.budgetForm.invalid || !this.selectedMonth) {
      this.budgetForm.markAllAsTouched();
      return;
    }

    const amount = Number(this.budgetForm.value.amount);

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (this.budget) {
      this.budgetService.updateBudget(
        this.budget.id,
        { amount }
      ).subscribe({
        next: (response) => {
          this.budget = response;
          this.isEditing = false;
          this.isLoading = false;
          this.successMessage = 'Budget updated successfully!';
          this.cdr.markForCheck();
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage =
            error.error?.message || 'Unable to update budget.';
          this.cdr.markForCheck();
        }
      });

    } else {
      const [year, month] = this.selectedMonth.split('-').map(Number);

      this.budgetService.createBudget({
        amount,
        year,
        month
      }).subscribe({
        next: (response) => {
          this.budget = response;
          this.isEditing = false;
          this.isLoading = false;
          this.successMessage = 'Budget created successfully!';
          this.cdr.markForCheck();
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage =
            error.error?.message || 'Unable to create budget.';
          this.cdr.markForCheck();
        }
      });
    }
  }

  editBudget(): void {
    this.isEditing = true;
    this.successMessage = '';
  }

  cancelEdit(): void {
    this.isEditing = false;

    if (this.budget) {
      this.budgetForm.patchValue({
        amount: this.budget.amount
      });
    }
  }

  getPercentage(): number {
    return Math.min(this.budget?.percentageUsed || 0, 100);
  }
}
