using contactManagerAPI.Models;
using contactManagerAPI.DTO;

namespace contactManagerAPI.Services.ContactRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactDTO>> GetAllContacts(int UserID);
        Task<Contact?> GetContactByID(int ID);
        Task<int> CreateContact(ContactDTO req);
        Task<bool> DeactivateContact(int ID);
        Task<bool> UpdateContact(ContactDTO req);
        Task<bool> ContactExists(ContactDTO req);
    }
}
