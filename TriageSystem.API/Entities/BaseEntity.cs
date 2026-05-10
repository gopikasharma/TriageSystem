using System;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Entities;

public class BaseEntity : IAuditable
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? DeletedAt { get; set; }

}
