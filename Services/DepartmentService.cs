
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class DepartmentService
{
    private readonly DepartmentRepository _repository;

    public DepartmentService()
    {
        _repository = new DepartmentRepository();
    }

    // Display all departments
    public List<Department> GetAllDepartments()
    {
        return _repository.GetAll();
    }

    // Find department by ID
    public Department? GetDepartmentById(int departmentId)
    {
        if (departmentId <= 0)
        {
            throw new ArgumentException(
                "Department ID must be greater than zero.");
        }

        return _repository.GetById(departmentId);
    }

    // Add a new department
    public int AddDepartment(string departmentName, string? location)
    {
        if (string.IsNullOrWhiteSpace(departmentName))
        {
            throw new ArgumentException(
                "Department name cannot be empty.");
        }

        Department department = new Department
        {
            DepartmentName = departmentName.Trim(),
            Location = string.IsNullOrWhiteSpace(location)
                ? null
                : location.Trim(),
            IsActive = true
        };

        return _repository.Add(department);
    }

    // Update department details
    public bool UpdateDepartment(
        int departmentId,
        string departmentName,
        string? location)
    {
        if (departmentId <= 0)
        {
            throw new ArgumentException(
                "Department ID must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(departmentName))
        {
            throw new ArgumentException(
                "Department name cannot be empty.");
        }

        Department? existing = _repository.GetById(departmentId);

        if (existing == null)
        {
            return false;
        }

        existing.DepartmentName = departmentName.Trim();
        existing.Location = string.IsNullOrWhiteSpace(location)
            ? null
            : location.Trim();

        return _repository.Update(existing);
    }

    // Deactivate department
    public bool DeactivateDepartment(int departmentId)
    {
        if (departmentId <= 0)
        {
            throw new ArgumentException(
                "Department ID must be greater than zero.");
        }

        return _repository.Deactivate(departmentId);
    }
}

