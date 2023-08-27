namespace contactManagerAPI.Services.MiscRepository
{
    public interface IMiscRepository
    {
        Task<bool> AddContactNumber(ContactNumber ContactNumber);
        Task<bool> UpdateContactNumber(ContactNumber req);
        Task<IEnumerable<ContactNumber>> GetContactNumbers(int OwnerID);
        Task<ContactNumber?> GetContactNumberByID(
            int ID,
            int OwnerID,
            string OwnerType,
            string Type
        );
    }
}
