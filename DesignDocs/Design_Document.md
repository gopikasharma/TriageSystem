# AI Patient Appointment Prioritization System
## System Design & Architecture Specification

---

**Document Information:**
- **Author:** Gopika
- **Document Type:** Design Document  
- **Version:** 1.0
- **Date:** April 22, 2026
- **Project Duration:** April 1 – June 30, 2026

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Problem Statement](#2-problem-statement)
3. [System Features & User Roles](#3-system-features--user-roles)
4. [System Architecture](#4-system-architecture)
5. [Technical Design](#5-technical-design)
6. [Workflow Relationships & Dependencies](#6-workflow-relationships--dependencies)
7. [Assumptions & Limitations](#7-assumptions--limitations)

---

## 1. Project Overview

The **AI Patient Appointment Prioritization System** is a web-based healthcare application that helps hospitals and clinics intelligently prioritize patient appointments based on reported symptoms and medical urgency. By combining rule-based scoring with AI-assisted analysis, the system reduces manual triage effort, improves patient safety, and optimizes doctor scheduling.

### Technology Stack

#### Frontend
- Angular 15+ / TypeScript
- Angular Material
- Chart.js
- RxJS

#### Backend
- ASP.NET Core 7/8 Web API
- C#
- Entity Framework Core
- AutoMapper
- JWT Authentication

#### Database
- Microsoft SQL Server
- MySQL (alternative)
- Redis (optional cache)

#### AI / ML
- Azure Cognitive Services
- OpenAI GPT-4
- ML.NET (custom model)

---

## 2. Problem Statement

### The Core Problem

In high-footfall hospitals and clinics, appointment scheduling is largely manual. Receptionists and triage nurses rely on subjective judgment to assign urgency levels, leading to inconsistent prioritization, prolonged wait times for genuinely critical patients, and inefficient use of doctor capacity.

### Key Pain Points

| # | Pain Point | Impact |
|---|------------|--------|
| 1 | Manual and subjective triage decisions | Inconsistent priority assignment; risk of under-triaging serious cases |
| 2 | Unstructured symptom collection | Critical information is missed or recorded inconsistently |
| 3 | No unified dashboard for clinical staff | Doctors lack real-time insight into the priority queue |
| 4 | Long patient wait times | Patient dissatisfaction and risk to critical patients |
| 5 | Lack of auditable decision trail | No record of how and why appointments were prioritized |

### Target Users

- **Patients (500 – 1,000):** Self-report symptoms via an online intake form; receive appointment notifications
- **Reception Staff (5 – 10):** Register walk-in patients, manage intake data, and oversee the appointment queue
- **Nurses (10 – 20):** Review and clinically validate AI-generated priority scores before escalation
- **Doctors (15 – 25):** View the prioritized patient list and manage their appointment schedule via a dashboard
- **System Administrators (2 – 3):** Manage user accounts, system configuration, reporting, and AI model settings

---

## 3. System Features & User Roles

### Home Page Features
- Sign In / Sign Up options
- Doctor Booking Portal - Accept email and phone, recommend full registration for follow-up appointments
- Quick symptom checker (for emergency cases)
- System overview and features

### 3.1 Patient/User Role

#### Registration Features
- Complete profile setup (Name, Email, Phone, Gender, Date of Birth, Address)
- Medical history input (allergies, current medications, chronic conditions)
- Emergency contact information
- Insurance details (if applicable)

#### Appointment Management
- Select medical category/specialization
- Choose preferred doctor
- Input current symptoms using structured form
- Submit pain level (0-10 scale) and symptom duration
- View available appointment dates and time slots
- Receive AI-generated priority score and estimated wait time

#### Appointment History
- View upcoming appointments with date, time, and doctor details
- Access past appointment records and consultation notes
- Filter options: Upcoming, Past, Cancelled, Completed
- Download appointment summaries and prescriptions

#### Notifications System
- SMS/Email alerts for appointment confirmations
- Reminders 24 hours before appointment
- Priority level updates if condition changes
- Rescheduling notifications

### 3.2 Reception Staff Role

**Primary Function:** Communication between Nurse and Patient

#### Patient Management
- Register new walk-in patients
- Update existing patient information
- Verify insurance and contact details
- Handle patient inquiries and complaints

#### Daily Operations
- View today's appointments (with doctor filter)
- Manage appointment check-ins and check-outs
- Handle appointment cancellations and rescheduling
- Print patient appointment cards and instructions

#### Appointment Scheduling Capabilities
- Manually create appointments for patients who call or walk-in
- Override system suggestions if slots are full
- Manage the waiting list and call patients when slots become available
- Handle emergency appointments and urgent cases

#### Priority Queue Management
- Monitor the "Awaiting Scheduling" queue with real-time status updates
- Filter by priority level: Emergency, High, Medium, Low
- Track consultation status: Waiting, In Progress, Completed
- Manage overflow patients and waiting room capacity

### 3.3 Nurse Role

#### Appointment Validation
- Review AI-generated priority scores for clinical accuracy
- One-click validation to approve AI recommendations
- Override AI priority with clinical reasoning when necessary
- Document validation decisions with timestamps

#### Patient Triage
- Conduct initial patient assessments
- Update symptom information based on clinical observation
- Escalate emergency cases immediately to doctors
- Provide pre-consultation screening and vital signs recording

#### Workflow Management
- Close completed appointments and update patient status
- Generate final schedule based on validated AI priorities
- Communicate schedule changes to doctors and reception
- Manage patient flow to optimize doctor availability

### 3.4 Doctor Role

#### Daily Schedule
- View today's appointment list sorted by priority and time
- Access patient details by clicking on patient rows
- See AI priority reasoning and nurse validation notes
- View complete patient history and previous consultations

#### Consultation Management
- Update consultation details, diagnosis, and treatment plans
- Prescribe medications and order tests
- Schedule follow-up appointments
- Add clinical notes and recommendations

#### Patient Communication
- Send post-consultation instructions to patients
- Request additional tests or specialist referrals
- Update treatment progress notes

### 3.5 Admin Role

#### User Management
- Add new staff: Doctors, Nurses, Reception staff
- Auto-generate secure usernames and passwords
- Assign role-based permissions and access levels
- Manage user deactivation and role changes

#### System Configuration
- Add medical categories/specializations
- Create category-specific questionnaires and symptoms lists
- Configure AI priority scoring parameters
- Set up notification templates and schedules

#### Reporting & Analytics
- Generate system usage reports
- Monitor AI accuracy and validation rates
- Track appointment completion rates and wait times
- Export data for administrative review

### Priority Levels

| Level | Score Range | Meaning | Target SLA |
|-------|-------------|---------|------------|
| **Emergency** | 80 – 100 | Life-threatening; immediate clinical attention required | < 15 min |
| **High** | 60 – 79 | Significant discomfort or risk; urgent evaluation needed | < 1 hour |
| **Medium** | 30 – 59 | Moderate symptoms; prompt but non-urgent care | < 4 hours |
| **Low** | 0 – 29 | Mild or routine condition; routine appointment sufficient | Same / next day |

---

## 4. System Architecture

### High-Level Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Frontend      │◄──►│  Backend API    │◄──►│   Database      │
│                 │    │                 │    │                 │
│ Angular SPA     │    │ ASP.NET Core    │    │ MS SQL Server   │
│ Angular Material│    │ Web API (RESTful)│   │ Entity Framework│
│ Chart.js        │    │ JWT Auth        │    │ Redis Cache     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

All three layers communicate over HTTPS. The backend also calls external AI services.

### Component Overview

| Layer | Component | Responsibility |
|-------|-----------|----------------|
| **Client** | Patient Portal | Symptom intake form, appointment status, notifications |
|  | Staff Portal | Reception queue management, nurse triage validation |
|  | Doctor Dashboard | Prioritized patient list, appointment controls, analytics |
| **API Layer** | Auth Controller | JWT token issuance and validation |
|  | Patient Controller | Patient CRUD, symptom submission endpoints |
|  | Priority Controller | Triggers AI analysis, returns scored assessments |
|  | Appointment Controller | Scheduling, availability queries, notifications |
| **Service Layer** | AI Priority Service | Integrates OpenAI / Azure / ML.NET, returns PriorityAssessment |
|  | Notification Service | Sends email/SMS confirmations and reminders |
| **Data Layer** | ApplicationDbContext | EF Core ORM; manages all database sets |
|  | Redis Cache | Caches frequent reads (doctor availability, queue snapshots) |
| **External** | AI APIs | OpenAI GPT-4 / Azure Text Analytics / ML.NET local model |

---

## 5. Technical Design

### 5.1 Module Design

The system follows a **layered, modular architecture** across three Visual Studio / Angular projects:

#### Backend Projects (.NET Solution)
```
PatientPrioritizationSystem/
├── PatientPrioritization.API
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
├── PatientPrioritization.Core
│   ├── Models/
│   ├── Interfaces/
│   └── DTOs/
├── PatientPrioritization.Infrastructure
│   ├── Data/ (DbContext)
│   ├── Repositories/
│   └── Services/
└── PatientPrioritization.Tests
```

#### Frontend (Angular)
```
patient-portal/src/app/
├── modules/
│   ├── patient/
│   ├── staff/
│   ├── doctor/
│   └── admin/
├── shared/
│   ├── components/
│   └── pipes/
└── services/
    ├── auth.service.ts
    ├── patient.service.ts
    ├── symptom.service.ts
    └── appointment.service.ts
```

### 5.2 API Design

**Base URL:** `https://<host>/api/v1` | **Protocol:** HTTPS only | **Auth:** Bearer JWT in `Authorization` header | **Format:** JSON

#### Authentication Endpoints

| Method | Endpoint | Request Body | Response | Auth |
|--------|----------|--------------|----------|------|
| POST | `/auth/login` | `{ email, password }` | `{ token, expiresIn, role }` | Public |
| POST | `/auth/refresh` | `{ refreshToken }` | `{ token, expiresIn }` | Public |
| POST | `/auth/logout` | — | `204 No Content` | JWT |

#### Key API Endpoints

| Resource | Method | Endpoint | Description | Roles |
|----------|--------|----------|-------------|-------|
| **Patients** | GET | `/patients` | List all patients (paged) | Reception, Admin |
|  | POST | `/patients` | Register new patient | Patient (self), Reception |
|  | PUT | `/patients/{id}` | Update patient profile | Reception, Admin |
| **Symptoms** | POST | `/symptoms` | Submit symptom report → triggers AI scoring | Patient, Reception |
|  | GET | `/symptoms/{id}` | Get symptom report by ID | Nurse, Doctor, Admin |
| **Priority** | GET | `/priority/{symptomReportId}` | Get AI assessment for a symptom report | Nurse, Doctor |
|  | PUT | `/priority/{id}/validate` | Override AI priority with clinical validation | Nurse, Doctor |
|  | GET | `/priority/queue` | Get current priority-sorted queue | Doctor, Nurse, Reception |
| **Appointments** | GET | `/appointments` | List appointments (filtered by date/doctor/status) | Reception, Doctor, Admin |
|  | POST | `/appointments` | Create appointment | Reception |
|  | PUT | `/appointments/{id}` | Update appointment (reschedule, status change) | Reception, Doctor |

### 5.3 Data Flow

#### Primary Flow: Patient Symptom Submission → Appointment

1. **Patient submits symptom form (Angular Frontend)**
   - Patient fills in the structured intake form (chief complaint, symptoms, pain level, duration, medications, allergies) and clicks Submit.

2. **API receives and validates the request (POST /symptoms)**
   - The backend validates the payload (model validation + JWT auth check). A new `SymptomReport` record is persisted to the database.

3. **AI Priority Service is invoked (Infrastructure Layer)**
   - `ISymptomAnalysisService.AnalyzeSymptomsAsync()` forwards symptom data to the configured AI provider and receives a structured JSON response.

4. **PriorityAssessment is persisted and returned**
   - The assessment result is saved to the `PriorityAssessments` table and linked to the `SymptomReport`. The combined response is returned to the frontend.

5. **Nurse/Doctor validates priority (Optional clinical override)**
   - Clinical staff review the AI score in the dashboard. If they disagree, they submit a validated priority via `PUT /priority/{id}/validate`.

6. **Appointment is scheduled (POST /appointments)**
   - Reception staff creates an appointment using the effective priority score to select the earliest available slot.

7. **Patient receives notification**
   - `INotificationService` dispatches an email/SMS confirmation with appointment details.

#### Database Entity Relationships

| Table | Key Fields | Relationships |
|-------|------------|---------------|
| **Users** | Id, Email, PasswordHash, Role | One-to-one with Patients / Doctors |
| **Patients** | Id, FirstName, LastName, DOB, ContactInfo | One-to-many → SymptomReports, Appointments |
| **SymptomReports** | Id, PatientId, Symptoms, PainLevel, Duration, SubmittedAt | Many-to-one → Patients; One-to-one → PriorityAssessments |
| **PriorityAssessments** | Id, SymptomReportId, AIScore, PriorityLevel, Confidence, ValidatedBy | One-to-one → SymptomReports |
| **Appointments** | Id, PatientId, DoctorId, DateTime, Status, PriorityLevel | Many-to-one → Patients; Many-to-one → Doctors |
| **Doctors** | Id, UserId, Specialization, AvailabilitySchedule | One-to-many → Appointments |

### 5.4 Error Handling

#### HTTP Error Response Format
All API errors return a consistent JSON envelope:
```json
{
  "statusCode": 400,
  "error": "Bad Request",
  "message": "PainLevel must be between 0 and 10.",
  "traceId": "00-4bf92f3577b34da6a3ce929d0e0e4736-00f067aa0ba902b7-01",
  "timestamp": "2026-04-22T10:30:00Z"
}
```

#### Error Categories & Strategies

| Category | HTTP Status | Handling Strategy |
|----------|-------------|-------------------|
| **Validation Errors** | 400 Bad Request | ASP.NET Core Model Validation returns field-level error details |
| **Authentication Failure** | 401 Unauthorized | Invalid or expired JWT. Angular HttpInterceptor auto-refreshes the token |
| **Authorization Failure** | 403 Forbidden | User role does not permit the action |
| **Resource Not Found** | 404 Not Found | Returned when a requested patient, appointment, or report ID does not exist |
| **Business Logic Errors** | 422 Unprocessable Entity | E.g., scheduling a slot already booked |
| **AI Service Failure** | 503 Service Unavailable | Retry up to 3 times with exponential back-off, fall back to rule-based scoring |
| **Database Errors** | 500 Internal Server Error | EF Core exceptions caught in global middleware |

#### AI Fallback Strategy
When the external AI provider is unreachable, the system automatically falls back to the **rule-based PriorityCalculationService**, which computes a score using weighted factors: symptom severity (40%), pain level (30%), duration (20%), and age (10%). The resulting `PriorityAssessment` is flagged as `Source = "RuleEngine"` so clinical staff know that AI confidence is not available and manual validation is required.

---

## 6. Workflow Relationships & Dependencies

### Doctor-Nurse-Category Relationship
- Nurses are assigned to specific doctors or medical categories
- When nurses log in, they can select their assigned doctor
- The system displays all patients scheduled for that doctor on the current day
- Nurses have the authority to close appointments and manage the daily schedule

### Daily Schedule Generation Process

1. **AI generates initial priority scores** for symptom reports
2. **Nurses review and validate** AI recommendations  
3. **Nurses can reorder patients** based on clinical judgment with documented reasons
4. **Final schedule is generated** and shared with assigned doctor
5. **Reception staff manage patient check-ins** according to the validated schedule
6. **Doctors see the prioritized patient list** and begin consultations

### Inter-Role Communication Flow

```
Patient → Reception → Nurse → Doctor
   ↓         ↓         ↓       ↓
Symptom   Scheduling  Triage  Treatment
Input    Management Validation Delivery
```

---

## 7. Assumptions & Limitations

### Assumptions

| # | Assumption |
|---|------------|
| A1 | All users access the application via a modern web browser; no native mobile client is in scope for this phase |
| A2 | Patients are capable of self-reporting symptoms digitally or are assisted by reception staff |
| A3 | An active internet connection is available for AI API calls to OpenAI or Azure Cognitive Services |
| A4 | The hospital has a SQL Server or MySQL instance available for deployment |
| A5 | Initial priority scoring thresholds are defined by a clinical subject matter expert before go-live |
| A6 | Doctors operate on a fixed weekly schedule that is configured in the system |
| A7 | The AI model will receive structured, text-based symptom inputs; voice or image-based diagnosis is out of scope |
| A8 | No formal HL7 FHIR integration with an existing Hospital Management System (HMS) is required in this phase |

### Limitations

| # | Limitation | Mitigation |
|---|------------|------------|
| L1 | AI scoring accuracy depends on the quality and completeness of patient-entered symptom data | Mandatory fields and structured input forms reduce free-text ambiguity. Nurse validation step acts as a clinical safety net |
| L2 | The AI provider (OpenAI / Azure) is an external dependency; service outages affect real-time scoring | Rule-based fallback ensures the system continues to function and assign a priority score under degraded conditions |
| L3 | The system does not replace clinical judgment; it provides decision support only | All AI assessments include a confidence score and a mandatory clinical validation step |
| L4 | Real-time notifications require an SMTP/SMS gateway configuration that depends on the deployment environment | Notification service is abstracted behind `INotificationService`; can be swapped with any provider |
| L5 | Initial ML.NET model training requires historical labeled patient data that may not be immediately available | System starts with OpenAI/Azure integration; ML.NET custom model is an enhancement for a later phase |
| L6 | Full HIPAA / PDPA compliance audit is beyond the scope of this internship project | Core security controls are implemented as a baseline. A formal compliance review is recommended before production |
| L7 | The system is designed for single-hospital deployment; multi-tenancy is not supported in this version | Architecture uses schema isolation in the database; multi-tenant support can be added in a future phase |

---

**Document Footer:**
AI Patient Appointment Prioritization System | Design Document v1.0 | April 22, 2026 | Confidential – Internal Use Only