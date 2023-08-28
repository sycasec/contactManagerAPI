using contactManagerAPI.Data;
using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Services.MiscRepository;

namespace contactManagerAPI.Services.MiscRepository
{
    public class MiscRepository : IMiscRepository
    {
        private readonly DataContext _context;
        private readonly IAuditService _auditor;

        public MiscRepository(DataContext context, IAuditService auditService)
        {
            _context = context;
            _auditor = auditService;
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

        public async Task<ContactNumber?> GetContactNumberByID(
            int ID,
            int OwnerID,
            string OwnerType,
            string Type
        )
        {
            var result = await _context.ContactNumbers
                .Where(
                    a =>
                        a.OwnerID == OwnerID
                        && a.ID == ID
                        && a.OwnerType == OwnerType
                        && a.Type == Type
                )
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ContactNumber>> GetContactNumbers(int OwnerID)
        {
            var result = await _context.ContactNumbers
                .Where(a => a.OwnerID == OwnerID)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateContactNumber(ContactNumber req)
        {
            var existingNumber = await _context.ContactNumbers
                .Where(
                    c =>
                        c.ID == req.ID
                        && c.OwnerID == req.OwnerID
                        && c.OwnerType == req.OwnerType
                        && c.Type == req.Type
                )
                .FirstOrDefaultAsync();

            if (existingNumber != null)
            {
                existingNumber.Number = req.Number;
                existingNumber.UpdatedBy = req.OwnerID;
                existingNumber.UpdatedOn = DateTime.UtcNow;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
