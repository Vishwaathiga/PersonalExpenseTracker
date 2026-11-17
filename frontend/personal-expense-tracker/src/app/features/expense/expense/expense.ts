
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  Expense,
  ExpenseService
} from '../../../services/expense';

import {
  CategoryService,
  Category
} from '../../../services/category';

@Component({
  selector: 'app-expense',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './expense.html',
  styleUrl: './expense.css'
})
export class ExpenseComponent implements OnInit {

  private fb = inject(FormBuilder);
  private expenseService = inject(ExpenseService);
  private categoryService = inject(CategoryService);

  expenses: Expense[] = [];
  categories: Category[] = [];

  isEditing = false;
  editingExpenseId: number | null = null;
  isLoading = false;

  successMessage = '';
  errorMessage = '';

  expenseForm = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(100)]],
    amount: [null as number | null, [Validators.required, Validators.min(0.01)]],
    description: [''],
    expenseDate: ['', Validators.required],
    categoryId: [null as number | null]
  });

  ngOnInit(): void {
    this.loadExpenses();
    this.loadCategories();
  }

  // Load all expenses
  loadExpenses(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.expenseService.getAllExpenses().subscribe({
      next: (response) => {
        this.expenses = response;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Unable to load expenses.';
        this.isLoading = false;
      }
    });
  }

  // Load default and custom categories
  loadCategories(): void {
    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: () => {
        this.errorMessage = 'Unable to load categories.';
      }
    });
  }

  // Add or update expense
  saveExpense(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.expenseForm.invalid) {
      this.expenseForm.markAllAsTouched();
      return;
    }

    const formValue = this.expenseForm.getRawValue();

    const request = {
      title: formValue.title!.trim(),
      amount: Number(formValue.amount),
      description: formValue.description?.trim() || null,
      expenseDate: formValue.expenseDate!,
      categoryId: formValue.categoryId
        ? Number(formValue.categoryId)
        : null
    };

    this.isLoading = true;

    if (this.isEditing && this.editingExpenseId !== null) {
      this.expenseService.updateExpense(
        this.editingExpenseId,
        request
      ).subscribe({
        next: (response) => {
          this.successMessage = response.message;
          this.resetForm();
          this.loadExpenses();
        },
        error: (error) => {
          this.errorMessage =
            error.error?.message || 'Unable to update expense.';
          this.isLoading = false;
        }
      });
    } else {
      this.expenseService.createExpense(request).subscribe({
        next: () => {
          this.successMessage = 'Expense added successfully.';
          this.resetForm();
          this.loadExpenses();
        },
        error: (error) => {
          this.errorMessage =
            error.error?.message || 'Unable to add expense.';
          this.isLoading = false;
        }
      });
    }
  }

  // Edit existing expense
  editExpense(expense: Expense): void {
    this.isEditing = true;
    this.editingExpenseId = expense.id;

    this.expenseForm.patchValue({
      title: expense.title,
      amount: expense.amount,
      description: expense.description || '',
      expenseDate: expense.expenseDate.substring(0, 10),
      categoryId: expense.categoryId ?? null
    });

    this.successMessage = '';
    this.errorMessage = '';
  }

  // Delete expense
  deleteExpense(id: number): void {
    if (!confirm('Are you sure you want to delete this expense?')) {
      return;
    }

    this.isLoading = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.expenseService.deleteExpense(id).subscribe({
      next: (response) => {
        this.successMessage = response.message;
        this.loadExpenses();
      },
      error: (error) => {
        this.errorMessage =
          error.error?.message || 'Unable to delete expense.';
        this.isLoading = false;
      }
    });
  }

  cancelEdit(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.expenseForm.reset();
    this.isEditing = false;
    this.editingExpenseId = null;
  }
}
