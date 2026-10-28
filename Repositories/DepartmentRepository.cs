
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class DepartmentRepository
{
    // 1. Display all departments
    public List<Department> GetAll()
    {
        List<Department> departments = new List<Department>();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT DepartmentId, DepartmentName, Location, IsActive
            FROM Departments
            ORDER BY DepartmentId;";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            departments.Add(new Department
            {
                DepartmentId = reader.GetInt32(0),
                DepartmentName = reader.GetString(1),
                Location = reader.IsDBNull(2) ? null : reader.GetString(2),
                IsActive = reader.GetBoolean(3)
            });
        }

        return departments;
    }

    // 2. Find a department by ID
    public Department? GetById(int departmentId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT DepartmentId, DepartmentName, Location, IsActive
            FROM Departments
            WHERE DepartmentId = @DepartmentId;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@DepartmentId", departmentId);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Department
            {
                DepartmentId = reader.GetInt32(0),
                DepartmentName = reader.GetString(1),
                Location = reader.IsDBNull(2) ? null : reader.GetString(2),
                IsActive = reader.GetBoolean(3)
            };
        }

        return null;
    }

    // 3. Add a department
    public int Add(Department department)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Departments (DepartmentName, Location, IsActive)
            VALUES (@DepartmentName, @Location, @IsActive);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@DepartmentName", department.DepartmentName);

        command.Parameters.AddWithValue(
            "@Location", (object?)department.Location ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@IsActive", department.IsActive);

        return (int)command.ExecuteScalar()!;
    }

    // 4. Update a department
    public bool Update(Department department)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Departments
            SET DepartmentName = @DepartmentName,
                Location = @Location,
                IsActive = @IsActive
            WHERE DepartmentId = @DepartmentId;";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue(
            "@DepartmentId", department.DepartmentId);

        command.Parameters.AddWithValue(
            "@DepartmentName", department.DepartmentName);

        command.Parameters.AddWithValue(
            "@Location", (object?)department.Location ?? DBNull.Value);

        command.Parameters.AddWithValue(
            "@IsActive", department.IsActive);

        return command.ExecuteNonQuery() > 0;
    }

    // 5. Deactivate a department
    public bool Deactivate(int departmentId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Departments
            SET IsActive = 0
            WHERE DepartmentId = @DepartmentId
              AND IsActive = 1;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@DepartmentId", departmentId);

        return command.ExecuteNonQuery() > 0;
    }
}
