namespace contactManagerAPI.DTO
{
    public class UserAuthDTO
    {
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
    }
}
