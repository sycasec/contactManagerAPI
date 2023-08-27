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

        public bool ContactExists(ContactDTO req)
        {
            bool exists = _context.Contacts.Any(
                c =>
                    c.UserID == req.UserID
                    && c.FirstName == req.FirstName
                    && c.LastName == req.LastName
                    && c.CompanyID == req.CompanyID
            );

            return exists;
        }

        public async Task<bool> CreateContact(ContactDTO req)
        {
            if (!ContactExists(req))
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
                        AddedOn = DateTime.Now,
                        IsDeleted = 0
                    };

                _ = await _context.Contacts.AddAsync(newContact);
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<IEnumerable<Contact>> GetAllContacts(int UserID)
        {
            var res = await _context.Contacts.Where(a => a.UserID == UserID).ToListAsync();

            return res;
        }
    }
}
