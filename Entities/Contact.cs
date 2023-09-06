namespace contactManagerAPI.Entities
{
    public class Contact
    {
        public int ID { get; set; }
        public required int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? IsFavorite { get; set; }
        public int? IsBlocked { get; set; }
        public int? IsDeleted { get; set; }
    }
}
