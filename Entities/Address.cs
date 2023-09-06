namespace contactManagerAPI.Entities
{
    public class Address
    {
        public int ID { get; set; } = 0;
        public int? OwnerID { get; set; }
        public string OwnerType { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? IsDeleted { get; set; }
    }
}
