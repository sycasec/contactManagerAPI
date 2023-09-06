using System.Reflection;
using contactManagerAPI.Data;
using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        private int GenerateNewID()
        {
            int lastUserID = 0;
            if (_context.Users.Any())
            {
                lastUserID = _context.Users.Max(u => u.ID);
            }

            int newID = lastUserID + 1;
            return newID;
        }

        List<string> updateProperties = new List<string>
        {
            "FirstName",
            "MiddleName",
            "LastName",
            "BillingAddress",
            "DeliveryAddress"
        };

        public async Task<bool> CreateUser(User newUser)
        {
            _ = _context.Users.Add(newUser);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UserExists(User user)
        {
            return await _context.Users
                .Where(u => u.Username == user.Username || u.EmailAddress == user.EmailAddress)
                .AnyAsync();
        }

        public async Task<bool> DeleteUserByID(int ID)
        {
            var user = await _context.Users.FindAsync(ID);
            if (user != null)
            {
                user.IsDeleted = 1;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (user != null)
            {
                user.IsDeleted = 1;
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            List<User> userList = await _context.Users.ToListAsync();
            return userList;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress.Equals(email));
            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            return user;
        }

        public async Task<User?> GetUserByToken(int UserID, string EmailAddress)
        {
            return await _context.Users
                .Where(u => u.ID.Equals(UserID) && u.EmailAddress.Equals(EmailAddress))
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByID(int UserID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == UserID);
            return user;
        }

        public async Task<int> GetUserID(string username)
        {
            User? user = await GetUserByUsername(username);
            int ID = user is not null ? user.ID : 0;
            return ID;
        }

        public async Task<bool> UpdateUser(User existingUser, User updateUser)
        {
            foreach (var prop in updateProperties)
            {
                PropertyInfo propinfo = typeof(User).GetProperty(prop);
                string propValue = (string)propinfo.GetValue(updateUser);

                if (!string.IsNullOrEmpty(propValue))
                {
                    propinfo.SetValue(existingUser, propValue);
                }
            }
            existingUser.UpdatedBy = existingUser.ID;
            existingUser.UpdatedOn = DateTime.UtcNow;

            _ = await _context.SaveChangesAsync();
            return true;
        }
    }
}
