import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-doctor-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="min-h-screen bg-gray-50 p-10">
      <div class="max-w-4xl mx-auto bg-white p-8 rounded-lg shadow border-t-4 border-[#124B3E]">
        <div class="flex justify-between items-center mb-6">
          <h1 class="text-3xl font-bold text-[#124B3E]">Doctor Portal</h1>
          <button (click)="logout()" class="bg-red-500 text-white px-4 py-2 rounded">Logout</button>
        </div>
        <p class="text-lg">Welcome, <strong>Dr. {{ userName }}</strong>.</p>
        <p class="text-gray-500">Your secure clinical session is active.</p>
      </div>
    </div>
  `
})
export class DoctorDashboard implements OnInit {
  private accountService = inject(AccountService);
  private router = inject(Router);
  userName = '';

  ngOnInit() {
    this.accountService.currentUser$.subscribe(user => {
      if (user) this.userName = user.firstName;
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }
}