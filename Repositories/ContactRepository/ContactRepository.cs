using System.Reflection;
using contactManagerAPI.Data;
using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.ContactRepository
{
    /// <summary>
    /// Repository class for contact operations.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of the `ContactRepository` class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown if the context is null.</exception>
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

        List<string> updateProperties = new List<string>
        {
            "FirstName",
            "LastName",
            "Description",
            "EmailAddress",
            "IsFavorite"
        };

        /// <inheritdoc/>
        public Task<bool> ContactExists(Contact req)
        {
            var exists = _context.Contacts.AnyAsync(
                c =>
                    c.UserID == req.UserID
                    && c.FirstName == req.FirstName
                    && c.LastName == req.LastName
            );

            return exists;
        }

        /// <inheritdoc/>
        public async Task<int> CreateContact(Contact createRequest)
        {
            var contactExists = await ContactExists(createRequest);
            if (!contactExists)
            {
                _ = await _context.Contacts.AddAsync(createRequest);
                _ = await _context.SaveChangesAsync();
                return createRequest.ID;
            }
            return 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeactivateContact(int ContactID)
        {
            var contact = await _context.Contacts.FindAsync(ContactID);
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

        /// <inheritdoc/>
        public async Task<bool> UpdateContact(Contact existing, Contact updateRequest)
        {
            foreach (var propName in updateProperties)
            {
                PropertyInfo propInfo = typeof(Contact).GetProperty(propName);
                if (propName == "IsFavorite" || propName == "IsBlocked")
                {
                    int? propVal = (int?)propInfo.GetValue(updateRequest);
                    if (propVal.HasValue)
                        propInfo.SetValue(existing, propVal);
                }
                else
                {
                    string propValue = (string)propInfo.GetValue(updateRequest);

                    if (!string.IsNullOrEmpty(propValue))
                    {
                        propInfo.SetValue(existing, propValue);
                    }
                }
            }
            existing.UpdatedBy = updateRequest.UserID;
            existing.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<ICollection<Contact>> GetAllContacts(int UserID)
        {
            return await _context.Contacts.Where(c => c.UserID == UserID).ToListAsync();
        }
    }
}
