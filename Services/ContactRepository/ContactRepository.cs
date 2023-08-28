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

        public Task<bool> ContactExists(ContactDTO req)
        {
            var exists = _context.Contacts.AnyAsync(
                c =>
                    c.UserID == req.UserID
                    && c.FirstName == req.FirstName
                    && c.LastName == req.LastName
                    && c.CompanyID == req.CompanyID
            );

            return exists;
        }

        public async Task<int> CreateContact(ContactDTO req)
        {
            var contactExists = await ContactExists(req);
            if (!contactExists)
            {
                Contact newContact =
                    new()
                    {
                        ID = GenerateNewID(),
                        UserID = req.UserID,
                        FirstName = req.FirstName!,
                        LastName = req.LastName,
                        Description = req.Description,
                        JobRole = req.JobRole,
                        CompanyID = req.CompanyID,
                        BillingAddress = req.BillingAddress,
                        DeliveryAddress = req.DeliveryAddress,
                        EmailAddress = req.EmailAddress,
                        AddedBy = req.UserID,
                        AddedOn = DateTime.UtcNow,
                        IsDeleted = 0
                    };

                _ = await _context.Contacts.AddAsync(newContact);
                _ = await _context.SaveChangesAsync();
                return newContact.ID;
            }
            return 0;
        }

        public async Task<bool> DeactivateContact(int ID)
        {
            var contact = await _context.Contacts.FindAsync(ID);
            if (contact != null)
            {
                contact.IsDeleted = 1;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Contact?> GetContactByID(int ID)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ID == ID);
            return contact;
        }

        public async Task<bool> UpdateContact(ContactDTO req)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ID == req.ID);
            if (contact != null)
            {
                foreach (var prop in typeof(ContactDTO).GetProperties())
                {
                    if (
                        typeof(Contact).GetProperty(prop.Name) != null
                        && prop.GetValue(req) != null
                        && prop.Name != nameof(ContactDTO.Numbers)
                        && prop.Name != nameof(ContactDTO.ID)
                        && prop.Name != nameof(ContactDTO.UserID)
                    )
                    {
                        typeof(Contact)
                            .GetProperty(prop.Name)
                            ?.SetValue(contact, prop.GetValue(req));
                    }
                }
                await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<IEnumerable<ContactDTO>> GetAllContacts(int UserID)
        {
            List<ContactDTO> contactList = await _context.Contacts
                .Select(
                    c =>
                        new ContactDTO
                        {
                            ID = c.ID,
                            UserID = c.UserID,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Description = c.Description,
                            JobRole = c.JobRole,
                            BillingAddress = c.BillingAddress,
                            DeliveryAddress = c.DeliveryAddress,
                            EmailAddress = c.EmailAddress,
                            IsFavorite = c.IsFavorite
                        }
                )
                .ToListAsync();
            return contactList;
        }
    }
}
