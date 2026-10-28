
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class DoctorService
{
    private readonly DoctorRepository _repository;
    private readonly DepartmentRepository _departmentRepository;

    public DoctorService()
    {
        _repository = new DoctorRepository();
        _departmentRepository = new DepartmentRepository();
    }

    public List<Doctor> GetAllDoctors()
    {
        return _repository.GetAll();
    }

    public Doctor? GetDoctorById(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException("Doctor ID must be greater than zero.");

        return _repository.GetById(doctorId);
    }

    public int AddDoctor(Doctor doctor)
    {
        ValidateDoctor(doctor);

        if (_departmentRepository.GetById(doctor.DepartmentId) == null)
            throw new ArgumentException("Department does not exist.");

        doctor.FullName = doctor.FullName.Trim();
        doctor.Specialization = doctor.Specialization.Trim();
        doctor.Phone = doctor.Phone.Trim();
        doctor.Email = string.IsNullOrWhiteSpace(doctor.Email)
            ? null : doctor.Email.Trim();
        doctor.IsActive = true;

        return _repository.Add(doctor);
    }

    public bool UpdateDoctor(Doctor doctor)
    {
        if (doctor.DoctorId <= 0)
            throw new ArgumentException("Doctor ID must be greater than zero.");

        ValidateDoctor(doctor);

        Doctor? existing = _repository.GetById(doctor.DoctorId);

        if (existing == null)
            return false;

        if (!existing.IsActive)
            throw new InvalidOperationException("Inactive doctors cannot be updated.");

        if (_departmentRepository.GetById(doctor.DepartmentId) == null)
            throw new ArgumentException("Department does not exist.");

        doctor.FullName = doctor.FullName.Trim();
        doctor.Specialization = doctor.Specialization.Trim();
        doctor.Phone = doctor.Phone.Trim();
        doctor.Email = string.IsNullOrWhiteSpace(doctor.Email)
            ? null : doctor.Email.Trim();

        return _repository.Update(doctor);
    }

    public bool DeactivateDoctor(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException("Doctor ID must be greater than zero.");

        return _repository.Deactivate(doctorId);
    }

    private static void ValidateDoctor(Doctor doctor)
    {
        if (doctor == null)
            throw new ArgumentNullException(nameof(doctor));

        if (string.IsNullOrWhiteSpace(doctor.FullName))
            throw new ArgumentException("Doctor name is required.");

        if (string.IsNullOrWhiteSpace(doctor.Specialization))
            throw new ArgumentException("Specialization is required.");

        if (string.IsNullOrWhiteSpace(doctor.Phone))
            throw new ArgumentException("Phone number is required.");

        if (doctor.FullName.Trim().Length > 100)
            throw new ArgumentException("Doctor name cannot exceed 100 characters.");

        if (doctor.ConsultationFee < 0)
            throw new ArgumentException("Consultation fee cannot be negative.");

        if (doctor.DepartmentId <= 0)
            throw new ArgumentException("Select a valid department.");

        if (!string.IsNullOrWhiteSpace(doctor.Email) &&
            !doctor.Email.Contains('@'))
            throw new ArgumentException("Enter a valid email address.");
    }
}
