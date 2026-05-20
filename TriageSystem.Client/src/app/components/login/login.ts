import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './login.html'
})
export class Login {
  private accountService = inject(AccountService);
  private router = inject(Router);
  
  activeView: 'patient' | 'doctor' = 'patient';
  
  // NEW: Toggle between login and registration for the patient
  patientMode: 'login' | 'register' = 'login'; 
  
  errorMessage = '';

  doctorCredentials = { email: '', password: '' };
  
  // NEW: Credentials for patient login
  patientLoginCredentials = { email: '', password: '' };

  patientRegisterModel = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    role: 1 
  };

  switchView(view: 'patient' | 'doctor') {
    this.activeView = view;
    this.errorMessage = '';
  }

  // NEW: Toggle form method
  togglePatientMode() {
    this.patientMode = this.patientMode === 'login' ? 'register' : 'login';
    this.errorMessage = '';
  }

  processDoctorLogin() {
    this.accountService.login(this.doctorCredentials).subscribe({
      next: () => {
        this.errorMessage = '';
        this.router.navigateByUrl('/doctor-dashboard'); 
      },
      error: (error) => {
        this.errorMessage = 'Access Denied. Invalid email or password.';
      }
    });
  }

  // NEW: Process Patient Login
  processPatientLogin() {
    this.accountService.login(this.patientLoginCredentials).subscribe({
      next: () => {
        this.errorMessage = '';
        this.router.navigateByUrl('/patient-dashboard'); 
      },
      error: (error) => {
        this.errorMessage = 'Invalid email or password.';
      }
    });
  }

  processPatientRegistration() {
    this.accountService.register(this.patientRegisterModel).subscribe({
      next: () => {
        this.errorMessage = '';
        this.router.navigateByUrl('/patient-dashboard'); 
      },
      error: (error) => {
        this.errorMessage = typeof error.error === 'string' ? error.error : 'Registration failed. Please check your inputs.';
      }
    });
  }
}