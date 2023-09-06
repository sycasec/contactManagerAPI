using contactManagerAPI.Models.ContactModels;

namespace contactManagerAPI.Services.ContactServices
{
    public interface IContactService
    {
        Task<GetContactModel> CreateContact(int UserID, UpsertContactModel Req);
        Task<bool> DeactivateContact(int ContactID);
        Task<GetContactModel> UpdateContact(int ContactID, UpsertContactModel Req);
        Task<ICollection<GetContactModel>> GetUserContacts(int UserID);
        Task<GetContactModel> GetUserContact(int ContactID);
    }
}
