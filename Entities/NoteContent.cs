namespace contactManagerAPI.Entities
{
    public class NoteContent
    {
        public int ID { get; set; }
        public required int NoteID { get; set; }
        public required string Contents { get; set; }
        public int AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
