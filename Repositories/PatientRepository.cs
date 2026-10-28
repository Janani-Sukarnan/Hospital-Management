
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class PatientRepository
{
    // 1. Get all patients
    public List<Patient> GetAll()
    {
        List<Patient> patients = new List<Patient>();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT PatientId, FullName, DateOfBirth, Gender,
                   Phone, Email, Address, RegisteredAt, IsActive
            FROM Patients
            ORDER BY PatientId;";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            patients.Add(MapPatient(reader));
        }

        return patients;
    }

    // 2. Get a patient by ID
    public Patient? GetById(int patientId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT PatientId, FullName, DateOfBirth, Gender,
                   Phone, Email, Address, RegisteredAt, IsActive
            FROM Patients
            WHERE PatientId = @PatientId;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@PatientId", patientId);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return MapPatient(reader);
        }

        return null;
    }

    // 3. Add a new patient
    public int Add(Patient patient)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Patients
                (FullName, DateOfBirth, Gender, Phone, Email, Address)
            VALUES
                (@FullName, @DateOfBirth, @Gender, @Phone, @Email, @Address);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@FullName", patient.FullName);
        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
        command.Parameters.AddWithValue("@Gender", patient.Gender);
        command.Parameters.AddWithValue("@Phone", patient.Phone);
        command.Parameters.AddWithValue(
            "@Email", (object?)patient.Email ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@Address", (object?)patient.Address ?? DBNull.Value);

        return (int)command.ExecuteScalar()!;
    }

    // 4. Update patient details
    public bool Update(Patient patient)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Patients
            SET FullName = @FullName,
                DateOfBirth = @DateOfBirth,
                Gender = @Gender,
                Phone = @Phone,
                Email = @Email,
                Address = @Address,
                IsActive = @IsActive
            WHERE PatientId = @PatientId;";

        using SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@PatientId", patient.PatientId);
        command.Parameters.AddWithValue("@FullName", patient.FullName);
        command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth);
        command.Parameters.AddWithValue("@Gender", patient.Gender);
        command.Parameters.AddWithValue("@Phone", patient.Phone);
        command.Parameters.AddWithValue(
            "@Email", (object?)patient.Email ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@Address", (object?)patient.Address ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsActive", patient.IsActive);

        return command.ExecuteNonQuery() > 0;
    }

    // 5. Deactivate a patient
    public bool Deactivate(int patientId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Patients
            SET IsActive = 0
            WHERE PatientId = @PatientId AND IsActive = 1;";

        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@PatientId", patientId);

        return command.ExecuteNonQuery() > 0;
    }

    // Convert a database row into a Patient object
    private static Patient MapPatient(SqlDataReader reader)
    {
        return new Patient
        {
            PatientId = reader.GetInt32(0),
            FullName = reader.GetString(1),
            DateOfBirth = reader.GetDateTime(2),
            Gender = reader.GetString(3),
            Phone = reader.GetString(4),
            Email = reader.IsDBNull(5) ? null : reader.GetString(5),
            Address = reader.IsDBNull(6) ? null : reader.GetString(6),
            RegisteredAt = reader.GetDateTime(7),
            IsActive = reader.GetBoolean(8)
        };
    }
}
