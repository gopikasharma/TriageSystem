import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthResponse, LoginRequest } from '../models/auth';
import { map } from 'rxjs';
import { environment } from '../../environments/environment'; 
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = environment.apiUrl; 
  
  currentUser = signal<AuthResponse | null>(null);

  constructor(private http: HttpClient) { }

  login(credentials: LoginRequest) {
    return this.http.post<AuthResponse>(this.baseUrl + 'account/login', credentials).pipe(
      map(response => {
        if (response && response.token) {
          localStorage.setItem('triage-token', response.token);
          this.currentUser.set(response);
        }
        return response;
      })
    );
  }

  logout() {
    localStorage.removeItem('triage-token');
    this.currentUser.set(null);
  }
}