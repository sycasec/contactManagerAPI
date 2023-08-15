namespace contactManagerAPI
{
    public class NoteContents
    {
        public int ID { get; set; }
        public required int NoteID { get; set; }
        public required string Contents { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
