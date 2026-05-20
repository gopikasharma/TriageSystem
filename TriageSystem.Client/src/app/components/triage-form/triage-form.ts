import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TriageService } from '../../core/services/triage.service';

@Component({
  selector: 'app-triage-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './triage-form.html',
  styleUrl: './triage-form.css'
})
export class TriageForm {
  // Keeping your data structure
  appointmentData = {
    patientName: '',
    chiefComplaint: '',
    painLevel: 5,
    appointmentDate: '',
    priority: 'Normal' // This will be updated by the AI
  };

  aiResult = signal<any>(null);
  isLoading = signal(false);

  constructor(private triageService: TriageService) {}

  // This is the new bridge to your backend
  runAiTriage() {
    this.isLoading.set(true);
    const submission = {
      patientId: '00000000-0000-0000-0000-000000000000',
      chiefComplaint: this.appointmentData.chiefComplaint,
      detailedSymptoms: 'Submitted via booking portal',
      painLevel: this.appointmentData.painLevel,
      durationInDays: 1
    };

    this.triageService.evaluateSymptoms(submission).subscribe({
      next: (res) => {
        this.aiResult.set(res);
        // Automatically update your old UI's priority field!
        this.appointmentData.priority = res.suggestedPriority;
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }
}