namespace contactManagerAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int RoleID { get; set; }
        public int? AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
