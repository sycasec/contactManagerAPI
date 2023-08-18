namespace contactManagerAPI.DTO {
    public class UserAuthDTO {
        public string? Username { get;set;} = string.Empty;
        public string Password {get;set;} = string.Empty;
        public string? EmailAddress {get;set;} = string.Empty;
    }
}