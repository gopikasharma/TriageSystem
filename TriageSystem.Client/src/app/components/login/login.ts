import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './login.html'
})
export class Login {
  private accountService = inject(AccountService);
  activeView: 'patient' | 'doctor' = 'patient';

  errorMessage = '';

  doctorCredentials = {
    email: '',
    password: ''
  };

  switchView(view: 'patient' | 'doctor') {
    this.activeView = view;
    this.errorMessage = '';

  }
  processDoctorLogin() {
  this.accountService.login(this.doctorCredentials).subscribe({
    next: (response) =>{
      console.log('Login Successful!',response)
      this.errorMessage = '';
        alert('Connected to C# Backend Successfully!');
      },
      error: (error) => {
        console.error('Login Failed:', error);
        this.errorMessage = 'Access Denied. Please check your credentials or server connection.';
      }
  });
}
}