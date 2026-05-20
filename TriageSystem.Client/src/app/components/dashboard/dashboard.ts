import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../core/services/account.service';
import { AuthResponse } from '../../models/auth';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html'
})
export class Dashboard implements OnInit {
  private accountService = inject(AccountService);
  
  userEmail: string = '';
  userRole: string = '';

  ngOnInit() {
    this.accountService.currentUser$.subscribe({
      next: (user: AuthResponse | null) => {
        if (user) {
          this.userEmail = user.email;
          this.userRole = user.roles && user.roles.length > 0 ? user.roles[0] : '';
        }
      }
    });
  }
}