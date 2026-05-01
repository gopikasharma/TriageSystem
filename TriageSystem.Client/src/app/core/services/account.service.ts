import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:7015/api/account/';

  login(credentials: any) {
    return this.http.post(this.baseUrl + 'login', credentials);
  }
}