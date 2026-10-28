
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class PaymentRepository
{
    public List<Payment> GetAll()
    {
        List<Payment> payments = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT PaymentId, BillId, Amount, PaymentMethod,
                   PaymentDate, ReferenceNumber
            FROM Payments
            ORDER BY PaymentDate DESC;";

        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            payments.Add(MapPayment(reader));
        }

        return payments;
    }

    public Payment? GetById(int paymentId)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT PaymentId, BillId, Amount, PaymentMethod,
                   PaymentDate, ReferenceNumber
            FROM Payments
            WHERE PaymentId = @PaymentId;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@PaymentId", paymentId);

        using SqlDataReader reader = command.ExecuteReader();

        return reader.Read() ? MapPayment(reader) : null;
    }

    public List<Payment> GetByBillId(int billId)
    {
        List<Payment> payments = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT PaymentId, BillId, Amount, PaymentMethod,
                   PaymentDate, ReferenceNumber
            FROM Payments
            WHERE BillId = @BillId
            ORDER BY PaymentDate DESC;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@BillId", billId);

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            payments.Add(MapPayment(reader));
        }

        return payments;
    }

    public int Add(Payment payment)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO Payments
                (BillId, Amount, PaymentMethod, ReferenceNumber)
            VALUES
                (@BillId, @Amount, @PaymentMethod, @ReferenceNumber);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@BillId", payment.BillId);
        command.Parameters.AddWithValue("@Amount", payment.Amount);
        command.Parameters.AddWithValue(
            "@PaymentMethod", payment.PaymentMethod);

        command.Parameters.AddWithValue(
            "@ReferenceNumber",
            (object?)payment.ReferenceNumber ?? DBNull.Value);

        return (int)command.ExecuteScalar()!;
    }


public int AddAndUpdateBillStatus(Payment payment, string newStatus)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        using SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            string insertQuery = @"
            INSERT INTO Payments
                (BillId, Amount, PaymentMethod, ReferenceNumber)
            VALUES
                (@BillId, @Amount, @PaymentMethod, @ReferenceNumber);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int paymentId;

            using (SqlCommand command = new SqlCommand(
                insertQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@BillId", payment.BillId);
                command.Parameters.AddWithValue("@Amount", payment.Amount);
                command.Parameters.AddWithValue(
                    "@PaymentMethod", payment.PaymentMethod);
                command.Parameters.AddWithValue(
                    "@ReferenceNumber",
                    (object?)payment.ReferenceNumber ?? DBNull.Value);

                paymentId = (int)command.ExecuteScalar()!;
            }

            string updateQuery = @"
            UPDATE Bills
            SET Status = @Status
            WHERE BillId = @BillId
              AND Status <> 'Cancelled';";

            using (SqlCommand command = new SqlCommand(
                updateQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@Status", newStatus);
                command.Parameters.AddWithValue("@BillId", payment.BillId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected != 1)
                    throw new InvalidOperationException(
                        "Bill status could not be updated.");
            }

            transaction.Commit();

            return paymentId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    

    private static Payment MapPayment(SqlDataReader reader)
    {
        return new Payment
        {
            PaymentId = reader.GetInt32(0),
            BillId = reader.GetInt32(1),
            Amount = reader.GetDecimal(2),
            PaymentMethod = reader.GetString(3),
            PaymentDate = reader.GetDateTime(4),
            ReferenceNumber = reader.IsDBNull(5)
                ? null
                : reader.GetString(5)
        };
    }
}
