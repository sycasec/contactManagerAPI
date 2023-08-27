namespace contactManagerAPI.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public required int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? JobRole { get; set; }
        public int? CompanyID { get; set; }
        public string? EmailAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? UpdatedOn { get; set; }
        public int? IsDeleted { get; set; }
    }
}
