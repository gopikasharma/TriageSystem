namespace TriageSystem.API.Shared
{
    public enum BiologicalSex
    {
        Male,
        Female,
        Other
    }
    public enum Priority{
    Routine = 0,    // Stable
    lessUrgent = 1,     // Needs attention soon
    Emergent = 2,   // Life-threatening
    Immediate = 3   // Resuscitation required
}
}