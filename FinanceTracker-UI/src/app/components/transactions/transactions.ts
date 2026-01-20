import { Component, OnInit, NgZone, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Transaction as TransactionService } from '../../services/transaction';
import { TransactionReadDto, TransactionUpsertDto } from '../../models/transaction.model';
import { Category } from '../../models/category.model';
import { CategoryService } from '../../services/category';
import Swal from 'sweetalert2';

declare var bootstrap: any;

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './transactions.html',
})
export class Transactions implements OnInit {
  transactions: TransactionReadDto[] = [];
  categories: Category[] = [];

  isEditMode = false;
  currentId: string | null = null;
  originalTransaction: string = '';

  newTransaction: TransactionUpsertDto = {
    amount: 0,
    description: '',
    date: new Date().toISOString().split('T')[0],
    type: 1,
    categoryId: '',
  };

  constructor(
    private transactionService: TransactionService,
    private categoryService: CategoryService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.loadTransactions();
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.categories = res;
        this.cdr.markForCheck(); // إبلاغ أنجلر بوجود داتا جديدة للـ Select
      },
    });
  }

  loadTransactions() {
    this.transactionService.getAllTransactions().subscribe({
      next: (res) => {
        this.transactions = res;
        this.cdr.markForCheck();
      },
    });
  }

  // استخدام دالة بدلاً من Getter لتحكم أدق في الزرار
  checkIfChanged(): boolean {
    return this.originalTransaction !== JSON.stringify(this.newTransaction);
  }

  openAddModal() {
    this.isEditMode = false;
    this.currentId = null;
    this.resetForm();
    this.originalTransaction = JSON.stringify(this.newTransaction);
    this.cdr.markForCheck();
  }

  openEditModal(item: TransactionReadDto) {
    this.isEditMode = true;
    this.currentId = item.id;

    // ضبط البيانات
    this.newTransaction = {
      amount: Number(item.amount),
      description: item.description || '',
      date: item.date.split('T')[0],
      type: Number(item.type),
      categoryId: this.categories.find((c) => c.name === item.categoryName)?.id || '',
    };

    // حفظ النسخة الأصلية فوراً والمزامنة مع الـ UI
    this.originalTransaction = JSON.stringify(this.newTransaction);
    this.cdr.detectChanges(); // إجبار الـ UI على التحديث فوراً ليقرأ الحالة الجديدة للزرار
  }

  onSave() {
    if (!this.newTransaction.amount || !this.newTransaction.categoryId) return;

    const payload = {
      ...this.newTransaction,
      amount: Number(this.newTransaction.amount),
      type: Number(this.newTransaction.type),
    };

    const request =
      this.isEditMode && this.currentId
        ? this.transactionService.updateTransaction(this.currentId, payload)
        : this.transactionService.addTransaction(payload);

    request.subscribe({
      next: () => {
        this.closeModal();
        Swal.fire({ title: 'Success!', icon: 'success', timer: 1500, showConfirmButton: false });
        this.loadTransactions();
        if (!this.isEditMode) this.resetForm();
      },
    });
  }

  private closeModal() {
    const modalElement = document.getElementById('addModal');
    if (modalElement) {
      const modalInstance = bootstrap.Modal.getInstance(modalElement);
      if (modalInstance) modalInstance.hide();

      setTimeout(() => {
        document.querySelectorAll('.modal-backdrop').forEach((el) => el.remove());
        document.body.classList.remove('modal-open');
        document.body.style.overflow = 'auto';
        document.body.style.paddingRight = '0';

        modalElement.removeAttribute('aria-hidden');
        document
          .querySelectorAll('[aria-hidden="true"]')
          .forEach((el) => el.removeAttribute('aria-hidden'));

        this.cdr.markForCheck();
      }, 100);
    }
  }

  resetForm() {
    this.newTransaction = {
      amount: 0,
      description: '',
      date: new Date().toISOString().split('T')[0],
      type: 1,
      categoryId: '',
    };
  }

  deleteItem(id: string) {
    Swal.fire({
      title: 'Are you sure?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.transactionService.deleteTransaction(id).subscribe({
          next: () => {
            this.loadTransactions();
            Swal.fire('Deleted!', '', 'success');
          },
        });
      }
    });
  }
}
