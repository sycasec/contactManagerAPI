using contactManagerAPI.Data;

namespace contactManagerAPI.Services.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        private int GenerateNewID()
        {
            int lastContactID = 0;
            if (_context.Contacts.Any())
            {
                lastContactID = _context.Contacts.Max(u => u.ID);
            }

            int newID = lastContactID + 1;
            return newID;
        }

        public Task<bool> CreateContact(int UserID, ContactDTO req)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeactivateContact(int UserID, ContactDTO req)
        {
            throw new NotImplementedException();
        }

        public Task<Contact?> GetContact(int UserID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContact(int UserID, ContactDTO req)
        {
            throw new NotImplementedException();
        }
    }
}
