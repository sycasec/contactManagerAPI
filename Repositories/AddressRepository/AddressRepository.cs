using System.Reflection;
using contactManagerAPI.Data;
using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;
        List<string> updateProperties = new List<string>
        {
            "Type",
            "Street",
            "City",
            "State",
            "PostalCode"
        };

        public AddressRepository(DataContext context)
        {
            _context = context;
        }

        private int GenerateNewID()
        {
            int lastID = 0;
            if (_context.Addresses.Any())
                lastID = _context.Addresses.Max(c => c.ID);
            int newID = lastID + 1;
            return newID;
        }

        public Task<bool> AddressExists(Address address)
        {
            var exists = _context.Addresses.AnyAsync(
                a =>
                    a.OwnerID == address.OwnerID
                    && a.OwnerType == address.OwnerType
                    && a.Type == address.Type
            );

            return exists;
        }

        public async Task<bool> DeactivateAddress(int AddressID)
        {
            var address = await _context.Addresses.FindAsync(AddressID);
            if (address != null)
            {
                address.IsDeleted = 1;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CreateAddress(Address request)
        {
            var addressExists = await AddressExists(request);
            if (!addressExists)
            {
                request.ID = GenerateNewID();
                request.AddedBy = request.OwnerID;
                request.AddedOn = DateTime.UtcNow;
                _ = await _context.Addresses.AddAsync(request);
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Address?> GetEntityAddress(int OwnerID, string OwnerType, int AddressID)
        {
            var result = await _context.Addresses
                .Where(a => a.OwnerID == OwnerID && a.ID == AddressID && a.OwnerType == OwnerType)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<ICollection<Address>> GetEntityAddresses(int OwnerID, string OwnerType)
        {
            return await _context.Addresses.Where(a => a.OwnerID == OwnerID).ToListAsync();
        }

        public async Task<bool> UpdateAddress(Address existing, Address request)
        {
            foreach (var propName in updateProperties)
            {
                PropertyInfo propInfo = typeof(Address).GetProperty(propName);
                string propValue = (string)propInfo.GetValue(request);
                if (!string.IsNullOrEmpty(propValue))
                {
                    propInfo.SetValue(existing, propValue);
                }
            }

            existing.UpdatedBy = request.OwnerID;
            existing.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
