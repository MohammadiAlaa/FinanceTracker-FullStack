import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionReadDto, TransactionUpsertDto } from '../models/transaction.model'; // استيراد الموديل الصحيح

@Injectable({
  providedIn: 'root',
})
export class Transaction {
  private baseUrl = environment.apiUrl + '/Transactions';

  constructor(private http: HttpClient) {}

  getSummary(): Observable<any> {
    return this.http.get(`${this.baseUrl}/summary`);
  }

  getAllTransactions(): Observable<TransactionReadDto[]> {
    return this.http.get<TransactionReadDto[]>(this.baseUrl);
  }

  addTransaction(model: any): Observable<any> {
    return this.http.post(this.baseUrl, model);
  }

  updateTransaction(id: string, model: TransactionUpsertDto): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, model);
  }

  deleteTransaction(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}

