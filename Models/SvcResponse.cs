namespace contactManagerAPI.Models
{
    public class SvcResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
