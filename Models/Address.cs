namespace contactManagerAPI.Models
{
    public class Address
    {
        public int ID { get; set; } = 0;
        public int? OwnerID { get; set; }
        public string? OwnerType { get; set; }
        public string? Type { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int? isDeleted { get; set; }
    }
}
