namespace contactManagerAPI.Models
{
    public class AuditLog
    {
        public int ID { get; set; }
        public required DateTime Timestamp { get; set; }
        public int EntityID { get; set; }
        public required string Action { get; set; }
        public required string EntityType { get; set; }
        public required string Details { get; set; }
    }
}
