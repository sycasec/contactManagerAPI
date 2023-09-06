using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.ContactRepository
{
    public interface IContactRepository
    {
        Task<ICollection<Contact>> GetAllContacts(int UserID);
        Task<Contact?> GetContactByID(int ID);
        Task<int> CreateContact(Contact createRequest);
        Task<bool> DeactivateContact(int ContactID);
        Task<bool> UpdateContact(Contact existing, Contact updateRequest);
        Task<bool> ContactExists(Contact existsRequest);
    }
}
