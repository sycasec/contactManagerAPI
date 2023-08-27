using contactManagerAPI.Models;
using contactManagerAPI.DTO;

namespace contactManagerAPI.Services.ContactRepository
{
    public interface IContactRepository
    {
        Task<Contact?> GetContact(int UserID);
        Task<bool> CreateContact(int UserID, ContactDTO req);
        Task<bool> DeactivateContact(int UserID, ContactDTO req);
        Task<bool> UpdateContact(int UserID, ContactDTO req);
    }
}
