
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class AuditLogService
{
    private readonly AuditLogRepository _repository;

    public AuditLogService()
    {
        _repository = new AuditLogRepository();
    }

    public List<AuditLog> GetAllLogs()
    {
        return _repository.GetAll();
    }

    public List<AuditLog> GetLogsByEntity(
        string entityName,
        int entityId)
    {
        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("Entity name is required.");

        if (entityId <= 0)
            throw new ArgumentException(
                "Entity ID must be greater than zero.");

        return _repository.GetByEntity(
            entityName.Trim(), entityId);
    }

    public long LogAction(
        string entityName,
        int entityId,
        string action,
        string? details = null)
    {
        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("Entity name is required.");

        if (entityId <= 0)
            throw new ArgumentException(
                "Entity ID must be greater than zero.");

        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action is required.");

        AuditLog log = new AuditLog
        {
            EntityName = entityName.Trim(),
            EntityId = entityId,
            Action = action.Trim(),
            Details = string.IsNullOrWhiteSpace(details)
                ? null
                : details.Trim()
        };

        return _repository.Add(log);
    }
}
