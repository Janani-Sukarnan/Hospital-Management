
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;

namespace HospitalManagementSystem.Services;

public class ReportService
{
    public int GetTotalPatients()
    {
        return ExecuteCount(
            "SELECT COUNT(*) FROM Patients WHERE IsActive = 1;");
    }

    public int GetTotalDoctors()
    {
        return ExecuteCount(
            "SELECT COUNT(*) FROM Doctors WHERE IsActive = 1;");
    }

    public int GetTotalAppointments()
    {
        return ExecuteCount(
            "SELECT COUNT(*) FROM Appointments;");
    }

    public int GetScheduledAppointments()
    {
        return ExecuteCount(
            "SELECT COUNT(*) FROM Appointments WHERE Status = 'Scheduled';");
    }

    public int GetCompletedAppointments()
    {
        return ExecuteCount(
            "SELECT COUNT(*) FROM Appointments WHERE Status = 'Completed';");
    }

    public decimal GetTotalRevenue()
    {
        using SqlConnection connection =
            DatabaseHelper.CreateConnection();

        connection.Open();

        string query = @"
            SELECT COALESCE(SUM(Amount), 0)
            FROM Payments;";

        using SqlCommand command = new(query, connection);

        return Convert.ToDecimal(command.ExecuteScalar());
    }

    public decimal GetOutstandingBalance()
    {
        using SqlConnection connection =
            DatabaseHelper.CreateConnection();

        connection.Open();

        string query = @"
            SELECT COALESCE(
                SUM(B.TotalAmount - COALESCE(P.PaidAmount, 0)), 0)
            FROM Bills B
            OUTER APPLY
            (
                SELECT SUM(Amount) AS PaidAmount
                FROM Payments
                WHERE BillId = B.BillId
            ) P
            WHERE B.Status <> 'Cancelled';";

        using SqlCommand command = new(query, connection);

        return Convert.ToDecimal(command.ExecuteScalar());
    }

    private static int ExecuteCount(string query)
    {
        using SqlConnection connection =
            DatabaseHelper.CreateConnection();

        connection.Open();

        using SqlCommand command = new(query, connection);

        return Convert.ToInt32(command.ExecuteScalar());
    }
}

