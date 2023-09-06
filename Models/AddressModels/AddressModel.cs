namespace contactManagerAPI.Models.AddressModels
{
    public class AddressModel
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string OwnerType { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public DateTime? AddedOn { get; set; }
    }
}
