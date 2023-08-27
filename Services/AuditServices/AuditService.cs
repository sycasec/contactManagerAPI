using contactManagerAPI.Data;

namespace contactManagerAPI.Services.AuditServices
{
    public class AuditService : IAuditService
    {
        private readonly DataContext _context;

        public AuditService(DataContext context)
        {
            _context = context;
        }

        private int GenerateNewID()
        {
            int lastUserID = 0;
            if (_context.AuditLogs.Any())
            {
                lastUserID = _context.Users.Max(u => u.ID);
            }

            int newID = lastUserID + 1;
            return newID;
        }

        public async Task LogEvent(int EntityID, string Action, string EntityType, string Details)
        {
            AuditLog newLog =
                new()
                {
                    ID = GenerateNewID(),
                    Timestamp = DateTime.Now,
                    EntityID = EntityID,
                    Action = Action,
                    EntityType = EntityType,
                    Details = Details
                };
            _ = _context.AuditLogs.Add(newLog);
            await _context.SaveChangesAsync();
        }
    }
}
