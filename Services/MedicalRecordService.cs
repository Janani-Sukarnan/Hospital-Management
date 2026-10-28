
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class MedicalRecordService
{
    private readonly MedicalRecordRepository _recordRepository;
    private readonly AppointmentRepository _appointmentRepository;

    public MedicalRecordService()
    {
        _recordRepository = new MedicalRecordRepository();
        _appointmentRepository = new AppointmentRepository();
    }

    public List<MedicalRecord> GetAllMedicalRecords()
    {
        return _recordRepository.GetAll();
    }

    public MedicalRecord? GetMedicalRecordById(int medicalRecordId)
    {
        if (medicalRecordId <= 0)
            throw new ArgumentException(
                "Medical record ID must be greater than zero.");

        return _recordRepository.GetById(medicalRecordId);
    }

    public int AddMedicalRecord(MedicalRecord record)
    {
        ValidateRecord(record);

        Appointment? appointment =
            _appointmentRepository.GetById(record.AppointmentId);

        if (appointment == null)
            throw new ArgumentException("Appointment does not exist.");

        if (appointment.Status != "Completed")
            throw new InvalidOperationException(
                "Medical records can only be added to completed appointments.");

        record.Diagnosis = record.Diagnosis.Trim();

        record.TreatmentNotes =
            string.IsNullOrWhiteSpace(record.TreatmentNotes)
                ? null
                : record.TreatmentNotes.Trim();

        return _recordRepository.Add(record);
    }

    public bool UpdateMedicalRecord(MedicalRecord record)
    {
        ValidateRecord(record);

        if (record.MedicalRecordId <= 0)
            throw new ArgumentException(
                "Medical record ID must be greater than zero.");

        MedicalRecord? existing =
            _recordRepository.GetById(record.MedicalRecordId);

        if (existing == null)
            return false;

        record.Diagnosis = record.Diagnosis.Trim();

        record.TreatmentNotes =
            string.IsNullOrWhiteSpace(record.TreatmentNotes)
                ? null
                : record.TreatmentNotes.Trim();

        return _recordRepository.Update(record);
    }

    private static void ValidateRecord(MedicalRecord record)
    {
        if (record == null)
            throw new ArgumentNullException(nameof(record));

        if (record.AppointmentId <= 0)
            throw new ArgumentException("Select a valid appointment.");

        if (string.IsNullOrWhiteSpace(record.Diagnosis))
            throw new ArgumentException("Diagnosis is required.");

        if (record.Diagnosis.Trim().Length > 1000)
            throw new ArgumentException(
                "Diagnosis cannot exceed 1000 characters.");

        if (record.FollowUpDate.HasValue &&
            record.FollowUpDate.Value.Date < DateTime.Today)
        {
            throw new ArgumentException(
                "Follow-up date cannot be in the past.");
        }
    }
}
