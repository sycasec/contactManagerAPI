namespace contactManagerAPI.Entities
{
    public class ContactNumber
    {
        public int ID { get; set; } = 0;
        public int? OwnerID { get; set; }
        public string? OwnerType { get; set; }
        public string? Number { get; set; }
        public string? Type { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
