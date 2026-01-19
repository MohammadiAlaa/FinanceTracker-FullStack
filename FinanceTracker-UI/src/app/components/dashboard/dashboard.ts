import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Auth } from '../../services/auth';
import { Router } from '@angular/router';
import { Transaction as TransactionService } from '../../services/transaction';
import Chart from 'chart.js/auto';


@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  @ViewChild('myChart') chartCanvas!: ElementRef;
  chart: any;

  summaryData: TransactionSummary = {
    totalIncome: 0,
    totalExpenses: 0,
    balance: 0,
  };

  username: string | null = '';

  constructor(
    private transactionService: TransactionService,
    private authService: Auth,
    private router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.username = localStorage.getItem('username');
    this.loadSummary();
  }

  loadSummary() {
    this.transactionService.getSummary().subscribe({
      next: (data: any) => {
        console.log('Raw Data from API:', data);

        this.summaryData = {
          totalIncome: data.totalIncome !== undefined ? data.totalIncome : data.TotalIncome || 0,
          totalExpenses:
            data.totalExpenses !== undefined ? data.totalExpenses : data.TotalExpenses || 0,
          balance: data.balance !== undefined ? data.balance : data.Balance || 0,
        };

        console.log('Mapped summaryData:', this.summaryData);

        this.cdr.detectChanges();
        this.createChart();
      },
      error: (err) => {
        console.error('Error fetching summary', err);
      },
    });
  }

  createChart() {
    setTimeout(() => {
      if (!this.chartCanvas) return;

      if (this.chart) {
        this.chart.destroy();
      }

      this.chart = new Chart(this.chartCanvas.nativeElement, {
        type: 'doughnut',
        data: {
          labels: ['Income', 'Expenses'],
          datasets: [
            {
              label: 'Financial Summary',
              data: [this.summaryData.totalIncome, this.summaryData.totalExpenses],
              backgroundColor: ['#198754', '#dc3545'],
              hoverOffset: 4,
            },
          ],
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
        },
      });
    }, 0);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
