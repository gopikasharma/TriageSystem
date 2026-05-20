import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Dashboard } from './components/dashboard/dashboard';
import { authGuard } from './core/guards/guard';
import { PatientDashboard } from './components/dashboard/patient-dashboard';
import { DoctorDashboard } from './components/dashboard/doctor.dashboard';

export const routes: Routes = [
  { path: '', component: Login },
  { path: 'login', component: Login },
  

  { 
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'patient-dashboard', component: PatientDashboard },
      { path: 'doctor-dashboard', component: DoctorDashboard }
    ]
  },
  
  { path: '**', redirectTo: 'login', pathMatch: 'full' }
];