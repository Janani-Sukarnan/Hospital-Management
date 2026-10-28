
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class MedicalRecordRepository
{
    public List<MedicalRecord> GetAll()
    {
        List<MedicalRecord> records = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT MedicalRecordId, AppointmentId, Diagnosis,
                   TreatmentNotes, FollowUpDate, CreatedAt
            FROM MedicalRecords
            ORDER BY MedicalRecordId;";

        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            records.Add(MapRecord(reader));

        return records;
    }

    public MedicalRecord? GetById(int medicalRecordId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT MedicalRecordId, AppointmentId, Diagnosis,
                   TreatmentNotes, FollowUpDate, CreatedAt
            FROM MedicalRecords
            WHERE MedicalRecordId = @MedicalRecordId;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@MedicalRecordId", medicalRecordId);

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapRecord(reader) : null;
    }

    public int Add(MedicalRecord record)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO MedicalRecords
                (AppointmentId, Diagnosis, TreatmentNotes, FollowUpDate)
            VALUES
                (@AppointmentId, @Diagnosis, @TreatmentNotes, @FollowUpDate);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@AppointmentId", record.AppointmentId);
        command.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
        command.Parameters.AddWithValue(
            "@TreatmentNotes", (object?)record.TreatmentNotes ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@FollowUpDate", (object?)record.FollowUpDate ?? DBNull.Value);

        return (int)command.ExecuteScalar()!;
    }

    public bool Update(MedicalRecord record)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE MedicalRecords
            SET Diagnosis = @Diagnosis,
                TreatmentNotes = @TreatmentNotes,
                FollowUpDate = @FollowUpDate
            WHERE MedicalRecordId = @MedicalRecordId;";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue(
            "@MedicalRecordId", record.MedicalRecordId);
        command.Parameters.AddWithValue("@Diagnosis", record.Diagnosis);
        command.Parameters.AddWithValue(
            "@TreatmentNotes", (object?)record.TreatmentNotes ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@FollowUpDate", (object?)record.FollowUpDate ?? DBNull.Value);

        return command.ExecuteNonQuery() > 0;
    }

    private static MedicalRecord MapRecord(SqlDataReader reader)
    {
        return new MedicalRecord
        {
            MedicalRecordId = reader.GetInt32(0),
            AppointmentId = reader.GetInt32(1),
            Diagnosis = reader.GetString(2),
            TreatmentNotes = reader.IsDBNull(3) ? null : reader.GetString(3),
            FollowUpDate = reader.IsDBNull(4)
                ? null
                : reader.GetDateTime(4),
            CreatedAt = reader.GetDateTime(5)
        };
    }
}
