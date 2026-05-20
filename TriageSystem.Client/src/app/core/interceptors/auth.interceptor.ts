import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { take } from 'rxjs/operators';
import { AuthResponse } from '../../models/auth';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);
  let token = '';

  // Take 1 safely gets the current value without keeping an open subscription
  accountService.currentUser$.pipe(take(1)).subscribe({
    next: (user: AuthResponse | null) => {
      if (user && user.token) {
        token = user.token;
      }
    }
  });

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(req);
};