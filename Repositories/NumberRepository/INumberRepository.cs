using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.NumberRepository
{
    public interface INumberRepository
    {
        Task<bool> ContactNumberExists(ContactNumber ContactNumber);
        Task<bool> AddContactNumber(ContactNumber ContactNumber);
        Task<bool> UpdateContactNumber(ContactNumber existing, ContactNumber request);
        Task<bool> DeactivateContactNumber(int NumberID);
        Task<ICollection<ContactNumber>> GetContactNumbers(int OwnerID, string OwnerType);
        Task<ContactNumber?> GetContactNumber(int ID, int OwnerID, string OwnerType);
    }
}
