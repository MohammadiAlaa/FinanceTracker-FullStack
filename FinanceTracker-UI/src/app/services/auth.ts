import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AuthResponse, LoginDto, RegisterDto } from '../models/auth.model';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private baseUrl = environment.apiUrl + '/Auth';
  constructor(private http: HttpClient) {}

  login(model: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, model).pipe(
      tap((res) => {
        if (res.isSuccess) {
          // حفظ التوكن في ذاكرة المتصفح
          localStorage.setItem('token', res.token);
          localStorage.setItem('username', res.username);
        }
      })
    );
  }
  register(model: RegisterDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, model);
  }
  
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
