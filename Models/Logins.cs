namespace contactManagerAPI.Models
{
    public class UserLogin
    {
        public int ID { get; set; }
        public required int UserID { get; set; }
        public required DateTime LogTime { get; set; }
        public required bool isLogin { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
