import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/category';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  newCategory: Partial<Category> = { name: '', icon: '📦', color: '#6c757d' };
  isLoading = false;

  constructor(
    private categoryService: CategoryService,
    private cdr: ChangeDetectorRef,
    private zone: NgZone,
  ) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.isLoading = true;
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.zone.run(() => {
          this.categories = res;
          this.isLoading = false;
          this.cdr.detectChanges();
        });
      },
      error: (err) => {
        this.zone.run(() => {
          this.isLoading = false;
          console.error('Error loading categories', err);
        });
      },
    });
  }

  onAdd() {
    if (!this.newCategory.name || !this.newCategory.icon) return;

    this.categoryService.addCategory(this.newCategory).subscribe({
      next: () => {
        this.zone.run(() => {
          this.loadCategories();
          this.newCategory = { name: '', icon: '📦', color: '#6c757d' };
          Swal.fire('Added!', 'New category created.', 'success');
        });
      },
      error: (err) => {
        Swal.fire('Error', 'Failed to add category', 'error');
      },
    });
  }

  onDelete(id: string) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'All transactions in this category will be affected!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.categoryService.deleteCategory(id).subscribe({
          next: () => {
            this.zone.run(() => {
              this.loadCategories();
              Swal.fire('Deleted!', 'Category removed.', 'success');
            });
          },
          error: () => {
            Swal.fire('Error', 'Could not delete category', 'error');
          },
        });
      }
    });
  }
}
