using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Scheduling.Domain;

public class SchedulingPractitionerReadModel
{
    [Key]
    public Guid PractitionerId { get; private set; }
    public bool IsActive { get; private set; }

    private SchedulingPractitionerReadModel() { }

    public SchedulingPractitionerReadModel(Guid practitionerId, bool isActive)
    {
        PractitionerId = practitionerId;
        IsActive = isActive;
    }

    public void UpdateActiveStatus(bool isActive)
    {
        IsActive = isActive;
    }
}