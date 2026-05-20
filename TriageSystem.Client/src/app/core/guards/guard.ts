import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map } from 'rxjs/operators';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  return accountService.currentUser$.pipe(
    map(user => {
      if (user) {
        return true; // User exists, allow access
      }
      // No user, redirect to login
      router.navigate(['/login']);
      return false; 
    })
  );
};