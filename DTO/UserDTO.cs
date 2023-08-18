namespace contactManagerAPI.DTO
{
    public class UserDTO
    {
        public int ID { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public IEnumerable<Address> Addresses { get; set; } = new List<Address>();
        public IEnumerable<PhoneNumber> Numbers { get; set; } = new List<PhoneNumber>();
        public string EmailAddress { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int RoleID { get; set; } = 0;
    }
}
