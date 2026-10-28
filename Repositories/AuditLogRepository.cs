
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Helpers;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Repositories;

public class AuditLogRepository
{
    public List<AuditLog> GetAll()
    {
        List<AuditLog> logs = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT AuditId, EntityName, EntityId, Action,
                   Details, CreatedAt
            FROM AuditLogs
            ORDER BY CreatedAt DESC;";

        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            logs.Add(MapAuditLog(reader));
        }

        return logs;
    }

    public List<AuditLog> GetByEntity(string entityName, int entityId)
    {
        List<AuditLog> logs = new();

        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            SELECT AuditId, EntityName, EntityId, Action,
                   Details, CreatedAt
            FROM AuditLogs
            WHERE EntityName = @EntityName
              AND EntityId = @EntityId
            ORDER BY CreatedAt DESC;";

        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@EntityName", entityName);
        command.Parameters.AddWithValue("@EntityId", entityId);

        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            logs.Add(MapAuditLog(reader));
        }

        return logs;
    }

    public long Add(AuditLog log)
    {
        using SqlConnection connection = DatabaseHelper.CreateConnection();
        connection.Open();

        string query = @"
            INSERT INTO AuditLogs
                (EntityName, EntityId, Action, Details)
            VALUES
                (@EntityName, @EntityId, @Action, @Details);

            SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

        using SqlCommand command = new(query, connection);

        command.Parameters.AddWithValue("@EntityName", log.EntityName);
        command.Parameters.AddWithValue("@EntityId", log.EntityId);
        command.Parameters.AddWithValue("@Action", log.Action);
        command.Parameters.AddWithValue(
            "@Details", (object?)log.Details ?? DBNull.Value);

        return (long)command.ExecuteScalar()!;
    }

    private static AuditLog MapAuditLog(SqlDataReader reader)
    {
        return new AuditLog
        {
            AuditId = reader.GetInt64(0),
            EntityName = reader.GetString(1),
            EntityId = reader.GetInt32(2),
            Action = reader.GetString(3),
            Details = reader.IsDBNull(4) ? null : reader.GetString(4),
            CreatedAt = reader.GetDateTime(5)
        };
    }
}
