
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class DoctorRepository
{
    // 1. Get all doctors
    public List<Doctor> GetAll()
    {
        List<Doctor> doctors = new List<Doctor>();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT DoctorId, FullName, Specialization, Phone,
                   Email, DepartmentId, ConsultationFee, IsActive
            FROM Doctors
            ORDER BY DoctorId;";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            doctors.Add(MapDoctor(reader));
        }

        return doctors;
    }

    // 2. Get a doctor by ID
    public Doctor? GetById(int doctorId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT DoctorId, FullName, Specialization, Phone,
                   Email, DepartmentId, ConsultationFee, IsActive
            FROM Doctors
            WHERE DoctorId = @DoctorId;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@DoctorId", doctorId);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return MapDoctor(reader);
        }

        return null;
    }

    // 3. Add a doctor
    public int Add(Doctor doctor)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Doctors
                (FullName, Specialization, Phone, Email,
                 DepartmentId, ConsultationFee, IsActive)
            VALUES
                (@FullName, @Specialization, @Phone, @Email,
                 @DepartmentId, @ConsultationFee, @IsActive);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FullName", doctor.FullName);
        command.Parameters.AddWithValue(
            "@Specialization", doctor.Specialization);
        command.Parameters.AddWithValue("@Phone", doctor.Phone);
        command.Parameters.AddWithValue(
            "@Email", (object?)doctor.Email ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@DepartmentId", doctor.DepartmentId);
        command.Parameters.AddWithValue(
            "@ConsultationFee", doctor.ConsultationFee);
        command.Parameters.AddWithValue("@IsActive", doctor.IsActive);

        return (int)command.ExecuteScalar()!;
    }

    // 4. Update a doctor
    public bool Update(Doctor doctor)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Doctors
            SET FullName = @FullName,
                Specialization = @Specialization,
                Phone = @Phone,
                Email = @Email,
                DepartmentId = @DepartmentId,
                ConsultationFee = @ConsultationFee,
                IsActive = @IsActive
            WHERE DoctorId = @DoctorId;";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
        command.Parameters.AddWithValue("@FullName", doctor.FullName);
        command.Parameters.AddWithValue(
            "@Specialization", doctor.Specialization);
        command.Parameters.AddWithValue("@Phone", doctor.Phone);
        command.Parameters.AddWithValue(
            "@Email", (object?)doctor.Email ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@DepartmentId", doctor.DepartmentId);
        command.Parameters.AddWithValue(
            "@ConsultationFee", doctor.ConsultationFee);
        command.Parameters.AddWithValue("@IsActive", doctor.IsActive);

        return command.ExecuteNonQuery() > 0;
    }

    // 5. Deactivate a doctor
    public bool Deactivate(int doctorId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Doctors
            SET IsActive = 0
            WHERE DoctorId = @DoctorId AND IsActive = 1;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@DoctorId", doctorId);

        return command.ExecuteNonQuery() > 0;
    }

    // Convert a database row into a Doctor object
    private static Doctor MapDoctor(SqlDataReader reader)
    {
        return new Doctor
        {
            DoctorId = reader.GetInt32(0),
            FullName = reader.GetString(1),
            Specialization = reader.GetString(2),
            Phone = reader.GetString(3),
            Email = reader.IsDBNull(4) ? null : reader.GetString(4),
            DepartmentId = reader.GetInt32(5),
            ConsultationFee = reader.GetDecimal(6),
            IsActive = reader.GetBoolean(7)
        };
    }
}
