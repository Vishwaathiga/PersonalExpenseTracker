
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CategoryService, Category } from '../../../services/category';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category.html',
  styleUrl: './category.css'
})
export class CategoryComponent implements OnInit {

  categories: Category[] = [];

  categoryName = '';
  editingId: number | null = null;

  errorMessage = '';
  successMessage = '';
  loading = false;

  constructor(
    private categoryService: CategoryService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.loading = true;

    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.errorMessage = 'Unable to load categories.';
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  saveCategory(): void {
    this.errorMessage = '';
    this.successMessage = '';

    const name = this.categoryName.trim();

    if (!name) {
      this.errorMessage = 'Please enter a category name.';
      return;
    }

    if (this.editingId === null) {
      this.categoryService.addCategory(name).subscribe({
        next: () => {
          this.successMessage = 'Category added successfully.';
          this.resetForm();
          this.loadCategories();
        },
        error: (error) => {
          this.errorMessage =
            error.error?.message || 'Unable to add category.';
          this.cdr.markForCheck();
        }
      });
    } else {
      this.categoryService.updateCategory(
        this.editingId,
        name
      ).subscribe({
        next: () => {
          this.successMessage = 'Category updated successfully.';
          this.resetForm();
          this.loadCategories();
        },
        error: (error) => {
          this.errorMessage =
            error.error?.message || 'Unable to update category.';
          this.cdr.markForCheck();
        }
      });
    }
  }

  editCategory(category: Category): void {
    this.editingId = category.id;
    this.categoryName = category.name;
    this.errorMessage = '';
    this.successMessage = '';
  }

  deleteCategory(category: Category): void {
    if (!confirm(`Are you sure you want to delete "${category.name}"?`)) {
      return;
    }

    this.categoryService.deleteCategory(category.id).subscribe({
      next: () => {
        this.successMessage = 'Category deleted successfully.';
        this.loadCategories();
      },
      error: () => {
        this.errorMessage = 'Unable to delete category.';
        this.cdr.markForCheck();
      }
    });
  }

  resetForm(): void {
    this.categoryName = '';
    this.editingId = null;
  }

  cancelEdit(): void {
    this.resetForm();
    this.errorMessage = '';
    this.successMessage = '';
  }
}
