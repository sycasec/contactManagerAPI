using contactManagerAPI.Models;
using contactManagerAPI.DTO;

namespace contactManagerAPI.Services.ContactRepository
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts(int UserID);
        Task<Contact?> GetContactByID(int ID);
        Task<bool> CreateContact(ContactDTO req);
        Task<bool> DeactivateContact(int ID);
        Task<bool> UpdateContact(ContactDTO req);
    }
}
