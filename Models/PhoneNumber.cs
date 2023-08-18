namespace contactManagerAPI.Models
{
    public class PhoneNumber
    {
        public int? ID { get; set; }
        public int? OwnerID { get; set; }
        public string? Number { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
