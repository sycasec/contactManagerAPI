namespace contactManagerAPI.Models.NumberModels
{
    public class NumberModel
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string OwnerType { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public DateTime? AddedOn { get; set; }
    }
}
