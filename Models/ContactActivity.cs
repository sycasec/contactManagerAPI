namespace contactManagerAPI.Models
{
    public class ContactActivity
    {
        public int ID { get; set; }
        public required int UserID { get; set; }
        public required int ContactID { get; set; }
        public required int ActivityType { get; set; }
        public required int ActivityID { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
