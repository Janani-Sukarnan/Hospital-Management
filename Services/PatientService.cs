using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

=
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class PatientService
{
    private readonly PatientRepository _repository;

    public PatientService()
    {
        _repository = new PatientRepository();
    }

    // 1. Display all patients
    public List<Patient> GetAllPatients()
    {
        return _repository.GetAll();
    }

    // 2. Find a patient by ID
    public Patient? GetPatientById(int patientId)
    {
        if (patientId <= 0)
        {
            throw new ArgumentException(
                "Patient ID must be greater than zero.");
        }

        return _repository.GetById(patientId);
    }

    // 3. Register a new patient
    public int RegisterPatient(Patient patient)
    {
        ValidatePatient(patient);

        patient.FullName = patient.FullName.Trim();
        patient.Gender = patient.Gender.Trim();
        patient.Phone = patient.Phone.Trim();

        patient.Email = string.IsNullOrWhiteSpace(patient.Email)
            ? null
            : patient.Email.Trim();

        patient.Address = string.IsNullOrWhiteSpace(patient.Address)
            ? null
            : patient.Address.Trim();

        return _repository.Add(patient);
    }

    // 4. Update patient details
    public bool UpdatePatient(Patient patient)
    {
        if (patient.PatientId <= 0)
        {
            throw new ArgumentException(
                "Patient ID must be greater than zero.");
        }

        ValidatePatient(patient);

        Patient? existing = _repository.GetById(patient.PatientId);

        if (existing == null)
        {
            return false;
        }

        if (!existing.IsActive)
        {
            throw new InvalidOperationException(
                "An inactive patient cannot be updated.");
        }

        patient.FullName = patient.FullName.Trim();
        patient.Gender = patient.Gender.Trim();
        patient.Phone = patient.Phone.Trim();

        patient.Email = string.IsNullOrWhiteSpace(patient.Email)
            ? null
            : patient.Email.Trim();

        patient.Address = string.IsNullOrWhiteSpace(patient.Address)
            ? null
            : patient.Address.Trim();

        patient.RegisteredAt = existing.RegisteredAt;

        return _repository.Update(patient);
    }

    // 5. Deactivate a patient
    public bool DeactivatePatient(int patientId)
    {
        if (patientId <= 0)
        {
            throw new ArgumentException(
                "Patient ID must be greater than zero.");
        }

        return _repository.Deactivate(patientId);
    }

    // Validate required patient details
    private static void ValidatePatient(Patient patient)
    {
        if (patient == null)
        {
            throw new ArgumentNullException(nameof(patient));
        }

        if (string.IsNullOrWhiteSpace(patient.FullName))
        {
            throw new ArgumentException(
                "Patient name cannot be empty.");
        }

        if (patient.DateOfBirth.Date > DateTime.Today)
        {
            throw new ArgumentException(
                "Date of birth cannot be in the future.");
        }

        string[] allowedGenders =
        {
            "Male", "Female", "Other", "Prefer not to say"
        };

        if (!allowedGenders.Contains(
            patient.Gender.Trim(),
            StringComparer.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                "Please enter a valid gender.");
        }

        if (string.IsNullOrWhiteSpace(patient.Phone))
        {
            throw new ArgumentException(
                "Phone number cannot be empty.");
        }

        if (patient.Phone.Trim().Length > 20)
        {
            throw new ArgumentException(
                "Phone number cannot exceed 20 characters.");
        }

        if (patient.FullName.Trim().Length > 100)
        {
            throw new ArgumentException(
                "Patient name cannot exceed 100 characters.");
        }

        if (!string.IsNullOrWhiteSpace(patient.Email) &&
            !patient.Email.Contains('@'))
        {
            throw new ArgumentException(
                "Please enter a valid email address.");
        }
    }
}
