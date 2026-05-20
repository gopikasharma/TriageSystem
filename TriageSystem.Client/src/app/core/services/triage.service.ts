import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SymptomSubmission, AssessmentResult } from '../../models/triage.model';

@Injectable({
  providedIn: 'root'
})
export class TriageService {
  private apiUrl = 'http://localhost:7015/api/triage'; 

  constructor(private http: HttpClient) { }

  evaluateSymptoms(submission: SymptomSubmission): Observable<AssessmentResult> {
    return this.http.post<AssessmentResult>(`${this.apiUrl}/evaluate`, submission);
  }
}