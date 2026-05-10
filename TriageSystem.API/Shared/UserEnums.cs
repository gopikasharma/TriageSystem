namespace TriageSystem.API.Shared
{
    public enum BiologicalSex
    {
        Male,
        Female,
        Other
    }

    public enum UserRole
    {
    Patient = 1,
    Reception = 2,
    Nurse = 3,
    Doctor = 4,
    Admin = 5
    }
    public enum PriorityLevel
    {
        Routine = 0,    // Stable
        LessUrgent = 1, // Needs attention soon
        Emergent = 2,   // Life-threatening
        Immediate = 3   // Resuscitation required
    }


    public enum AppointmentStatus
    {
        PendingValidation = 0, 
        Scheduled = 1,

        CheckedIn = 2,

        
        InProgress = 3,

        
        Completed = 4,

        Cancelled = 5
    }
}