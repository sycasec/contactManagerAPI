namespace contactManagerAPI.Services.AuditServices
{
    public interface IAuditService
    {
        Task LogEvent(int EntityID, string Action, string EntityType, string Details);
    }
}
