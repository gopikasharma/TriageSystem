import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { AuthResponse, LoginRequest } from '../../models/auth';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  // Ensure this matches your C# backend URL and port
  private baseUrl = 'https://localhost:7015/api/account/';

  // Holds the logged-in user's state across the app
  private currentUserSource = new BehaviorSubject<AuthResponse | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  login(credentials: LoginRequest) {
    return this.http.post<AuthResponse>(this.baseUrl + 'login', credentials).pipe(
      tap((response) => this.handleAuthentication(response))
    );
  }

  register(model: any) {
    return this.http.post<AuthResponse>(this.baseUrl + 'register', model).pipe(
      tap((response) => this.handleAuthentication(response))
    );
  }

  private handleAuthentication(response: AuthResponse) {
    if (response) {
      // Save token to local storage so the user stays logged in after a refresh
      localStorage.setItem('user', JSON.stringify(response));
      this.currentUserSource.next(response);
    }
  }

  setCurrentUser(user: AuthResponse) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}