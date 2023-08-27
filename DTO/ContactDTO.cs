namespace contactManagerAPI.DTO
{
    public class ContactDTO
    {
        public required int ID { get; set; }
        public required int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? JobRole { get; set; }
        public int? CompanyID { get; set; }
        public string? BillingAddress { get; set; }
        public string? DeliveryAddress { get; set; }
        public IEnumerable<ContactNumber>? Numbers { get; set; }
        public string? EmailAddress { get; set; }
    }
}
