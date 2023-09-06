using contactManagerAPI.Data;
using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.NumberRepository
{
    /// <summary>
    /// Repository class for Number operations.
    /// </summary>
    public class NumberRepository : INumberRepository
    {
        private readonly DataContext _context;

        public NumberRepository(DataContext context)
        {
            _context = context;
        }

        private int GenerateNewID()
        {
            int lastContactNumberID = 0;
            if (_context.ContactNumbers.Any())
            {
                lastContactNumberID = _context.ContactNumbers.Max(u => u.ID);
            }

            int newID = lastContactNumberID + 1;
            return newID;
        }

        public Task<bool> ContactNumberExists(ContactNumber ContactNumber)
        {
            var exists = _context.ContactNumbers.AnyAsync(
                a =>
                    a.OwnerID == ContactNumber.OwnerID
                    && a.OwnerType == ContactNumber.OwnerType
                    && a.Type == ContactNumber.Type
            );

            return exists;
        }

        public async Task<bool> AddContactNumber(ContactNumber req)
        {
            /*
            * input
            * OwnerID, OwnerType
            * Number, Type
            */
            var contactNumberExists = await ContactNumberExists(req);
            if (!contactNumberExists)
            {
                req.ID = GenerateNewID();
                req.AddedBy = req.OwnerID;
                req.AddedOn = DateTime.UtcNow;
                _ = await _context.ContactNumbers.AddAsync(req);
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeactivateContactNumber(int NumberID)
        {
            var number = await _context.ContactNumbers.FindAsync(NumberID);
            if (number != null)
            {
                number.IsDeleted = 1;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ContactNumber?> GetContactNumber(int ID, int OwnerID, string OwnerType)
        {
            var result = await _context.ContactNumbers
                .Where(a => a.OwnerID == OwnerID && a.ID == ID && a.OwnerType == OwnerType)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<ICollection<ContactNumber>> GetContactNumbers(
            int OwnerID,
            string OwnerType
        )
        {
            var result = await _context.ContactNumbers
                .Where(a => a.OwnerID == OwnerID && a.OwnerType == OwnerType)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateContactNumber(ContactNumber existing, ContactNumber request)
        {
            existing.Number = request.Number;
            existing.UpdatedBy = request.OwnerID;
            existing.UpdatedOn = DateTime.UtcNow;
            _ = await _context.SaveChangesAsync();
            return true;
        }
    }
}
