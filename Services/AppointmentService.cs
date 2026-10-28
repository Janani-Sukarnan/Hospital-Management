
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class AppointmentService
{
    private readonly AppointmentRepository _repository;
    private readonly PatientRepository _patientRepository;
    private readonly DoctorRepository _doctorRepository;

    public AppointmentService()
    {
        _repository = new AppointmentRepository();
        _patientRepository = new PatientRepository();
        _doctorRepository = new DoctorRepository();
    }

    public List<Appointment> GetAllAppointments()
    {
        return _repository.GetAll();
    }

    public Appointment? GetAppointmentById(int appointmentId)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Appointment ID must be greater than zero.");

        return _repository.GetById(appointmentId);
    }

    public int ScheduleAppointment(Appointment appointment)
    {
        ValidateAppointment(appointment);

        Patient? patient = _patientRepository.GetById(appointment.PatientId);
        if (patient == null || !patient.IsActive)
            throw new ArgumentException("Patient does not exist or is inactive.");

        Doctor? doctor = _doctorRepository.GetById(appointment.DoctorId);
        if (doctor == null || !doctor.IsActive)
            throw new ArgumentException("Doctor does not exist or is inactive.");

        if (appointment.AppointmentDate <= DateTime.Now)
            throw new ArgumentException("Appointment must be in the future.");

        if (_repository.HasDoctorConflict(
            appointment.DoctorId, appointment.AppointmentDate))
        {
            throw new InvalidOperationException(
                "The doctor already has an appointment at that date and time.");
        }

        appointment.Reason = string.IsNullOrWhiteSpace(appointment.Reason)
            ? null
            : appointment.Reason.Trim();

        appointment.Status = "Scheduled";

        return _repository.Add(appointment);
    }

    public bool UpdateAppointmentStatus(int appointmentId, string status)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Appointment ID must be greater than zero.");

        string[] allowedStatuses =
        {
            "Scheduled", "Completed", "Cancelled", "NoShow"
        };

        if (!allowedStatuses.Contains(
            status, StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid appointment status.");
        }

        Appointment? appointment = _repository.GetById(appointmentId);

        if (appointment == null)
            return false;

        if (appointment.Status != "Scheduled")
            throw new InvalidOperationException(
                "Only scheduled appointments can change status.");

        return _repository.UpdateStatus(
            appointmentId,
            allowedStatuses.First(s =>
                s.Equals(status, StringComparison.OrdinalIgnoreCase)));
    }

    private static void ValidateAppointment(Appointment appointment)
    {
        if (appointment == null)
            throw new ArgumentNullException(nameof(appointment));

        if (appointment.PatientId <= 0)
            throw new ArgumentException("Select a valid patient.");

        if (appointment.DoctorId <= 0)
            throw new ArgumentException("Select a valid doctor.");
    }
}
