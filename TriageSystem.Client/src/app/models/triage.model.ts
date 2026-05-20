export interface SymptomSubmission {
  patientId: string;
  chiefComplaint: string;
  detailedSymptoms: string;
  painLevel: number;
  durationInDays: number;
}

export interface AssessmentResult {
  aiScore: number;
  suggestedPriority: string;
  aiReasoning: string;
}