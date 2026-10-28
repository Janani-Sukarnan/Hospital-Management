
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class BillingRepository
{
    public List<Bill> GetAll()
    {
        List<Bill> bills = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT BillId, AppointmentId, TotalAmount, BillDate, Status
            FROM Bills
            ORDER BY BillDate DESC;";

        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            bills.Add(MapBill(reader));
        }

        return bills;
    }

    public Bill? GetById(int billId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT BillId, AppointmentId, TotalAmount, BillDate, Status
            FROM Bills
            WHERE BillId = @BillId;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@BillId", billId);

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapBill(reader) : null;
    }

    public int Add(Bill bill)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Bills (AppointmentId, TotalAmount, Status)
            VALUES (@AppointmentId, @TotalAmount, @Status);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue(
            "@AppointmentId", bill.AppointmentId);

        command.Parameters.AddWithValue(
            "@TotalAmount", bill.TotalAmount);

        command.Parameters.AddWithValue(
            "@Status", bill.Status);

        return (int)command.ExecuteScalar()!;
    }

    public bool UpdateStatus(int billId, string status)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            UPDATE Bills
            SET Status = @Status
            WHERE BillId = @BillId;";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@BillId", billId);
        command.Parameters.AddWithValue("@Status", status);

        return command.ExecuteNonQuery() > 0;
    }

    private static Bill MapBill(SqlDataReader reader)
    {
        return new Bill
        {
            BillId = reader.GetInt32(0),
            AppointmentId = reader.GetInt32(1),
            TotalAmount = reader.GetDecimal(2),
            BillDate = reader.GetDateTime(3),
            Status = reader.GetString(4)
        };
    }
}
