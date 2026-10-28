using System;

namespace HospitalManagementSystem.Models;

public class MedicalRecord
{
    public int MedicalRecordId { get; set; }

    public int AppointmentId { get; set; }

    public string Diagnosis { get; set; } = string.Empty;

    public string? TreatmentNotes { get; set; }

    public DateTime? FollowUpDate { get; set; }

    public DateTime CreatedAt { get; set; }
}

