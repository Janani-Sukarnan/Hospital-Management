
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class AppointmentRepository
{
    public List<Appointment> GetAll()
    {
        List<Appointment> appointments = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT AppointmentId, PatientId, DoctorId,
                   AppointmentDate, Reason, Status, CreatedAt
            FROM Appointments
            ORDER BY AppointmentDate;";

        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
            appointments.Add(MapAppointment(reader));

        return appointments;
    }

    public Appointment? GetById(int appointmentId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT AppointmentId, PatientId, DoctorId,
                   AppointmentDate, Reason, Status, CreatedAt
            FROM Appointments
            WHERE AppointmentId = @AppointmentId;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@AppointmentId", appointmentId);

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapAppointment(reader) : null;
    }

    public int Add(Appointment appointment)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Appointments
                (PatientId, DoctorId, AppointmentDate, Reason, Status)
            VALUES
                (@PatientId, @DoctorId, @AppointmentDate, @Reason, @Status);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@PatientId", appointment.PatientId);
        command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
        command.Parameters.AddWithValue(
            "@AppointmentDate", appointment.AppointmentDate);
        command.Parameters.AddWithValue(
            "@Reason", (object?)appointment.Reason ?? DBNull.Value);
        command.Parameters.AddWithValue("@Status", appointment.Status);

        return (int)command.ExecuteScalar()!;
    }

    public bool UpdateStatus(int appointmentId, string status)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Appointments
            SET Status = @Status
            WHERE AppointmentId = @AppointmentId;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@AppointmentId", appointmentId);
        command.Parameters.AddWithValue("@Status", status);

        return command.ExecuteNonQuery() > 0;
    }

    public bool HasDoctorConflict(int doctorId, DateTime appointmentDate)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT COUNT(*)
            FROM Appointments
            WHERE DoctorId = @DoctorId
              AND AppointmentDate = @AppointmentDate
              AND Status = 'Scheduled';";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@DoctorId", doctorId);
        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private static Appointment MapAppointment(SqlDataReader reader)
    {
        return new Appointment
        {
            AppointmentId = reader.GetInt32(0),
            PatientId = reader.GetInt32(1),
            DoctorId = reader.GetInt32(2),
            AppointmentDate = reader.GetDateTime(3),
            Reason = reader.IsDBNull(4) ? null : reader.GetString(4),
            Status = reader.GetString(5),
            CreatedAt = reader.GetDateTime(6)
        };
    }
}
