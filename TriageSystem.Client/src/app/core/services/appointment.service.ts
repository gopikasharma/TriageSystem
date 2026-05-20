import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AppointmentSummary {
  id: string;
  department: string;
  requestedDate: string;
  confirmedDate: string | null;
  status: string;
  doctorName: string;
}

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:7015/api/appointment/';

  getPatientAppointments(): Observable<AppointmentSummary[]> {
    return this.http.get<AppointmentSummary[]>(this.baseUrl + 'my-appointments');
  }
}