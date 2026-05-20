import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AccountService } from '../../core/services/account.service';
import { AppointmentService, AppointmentSummary } from '../../core/services/appointment.service';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-dashboard.html'
})
export class PatientDashboard implements OnInit {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  private appointmentService = inject(AppointmentService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  userName = '';
  currentView: 'list' | 'request' = 'list'; // Toggles the tabs
  appointments: AppointmentSummary[] = [];

  // Form State
  isSubmitting = false;
  assessmentResult: any = null;
  errorMessage = '';

  bookingForm = {
    department: '',
    appointmentDate: '',
    chiefComplaint: '',
    detailedSymptoms: '',
    painLevel: 0,
    durationInDays: 0
  };

  departments = ['General Medicine', 'Cardiology', 'Orthopedics', 'Neurology', 'Pediatrics'];

  ngOnInit() {
    this.accountService.currentUser$.subscribe(user => {
      if (user) {
        this.userName = user.firstName;
        this.loadAppointments();
      } else {
        this.router.navigateByUrl('/login');
      }
    });
  }

  loadAppointments() {
    this.appointmentService.getPatientAppointments().subscribe({
      next: (data) => {
        this.appointments = data;
        this.cdr.detectChanges();
      },
      error: (err) => console.error("Failed to load appointments", err)
    });
  }

  submitAppointment() {
    this.isSubmitting = true;
    this.errorMessage = '';

    const triagePayload = {
      chiefComplaint: this.bookingForm.chiefComplaint,
      detailedSymptoms: this.bookingForm.detailedSymptoms,
      painLevel: this.bookingForm.painLevel,
      durationInDays: this.bookingForm.durationInDays
    };

    this.http.post('https://localhost:7015/api/triage/submit-symptoms', triagePayload)
      .subscribe({
        next: (result: any) => {
          this.assessmentResult = result;
          this.isSubmitting = false;
          // Refresh the appointments list in the background so it's ready
          this.loadAppointments(); 
          this.cdr.detectChanges();
        },
        error: (error) => {
          this.errorMessage = 'Failed to submit appointment. Please try again.';
          this.isSubmitting = false;
          this.cdr.detectChanges();
        }
      });
  }

  resetForm() {
    this.assessmentResult = null;
    this.bookingForm = {
      department: '',
      appointmentDate: '',
      chiefComplaint: '',
      detailedSymptoms: '',
      painLevel: 0,
      durationInDays: 0
    };
    // Send them back to the list view after booking
    this.currentView = 'list';
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }
}