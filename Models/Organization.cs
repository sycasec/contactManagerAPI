namespace contactManagerAPI.Models
{
    public class Organization
    {
        public int ID { get; set; }
        public required string OrganizationName { get; set; }
        public string? OrganizationOwner { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OrganizationURL { get; set; }
        public required string City { get; set; }
        public required string CountryOrRegion { get; set; }
        public DateTime? LastActivity { get; set; }
        public required string Industry { get; set; }
        public required DateTime AddedOn { get; set; }
        public required int AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public required int isDeleted { get; set; }
    }
}
