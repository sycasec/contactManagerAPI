namespace contactManagerAPI.Services.ContactServices
{
    public interface IContactService
    {
        Task<SvcResponse<int>> CreateContact(ContactDTO req);
        Task<SvcResponse<string>> DeactivateContact(ContactDTO req);
        Task<SvcResponse<string>> UpdateContact(ContactDTO req);
        Task<SvcResponse<IEnumerable<ContactDTO>>> GetAllContacts(int UserID);
    }
}
