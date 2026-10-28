
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem.Helpers;

public static class DatabaseHelper
{
    private const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=HospitalDB;" +
        @"Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}

