using contactManagerAPI.DTO;
using contactManagerAPI.Data;

namespace contactManagerAPI.Services.UserRepository
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

        public async Task<bool> CreateUser(UserDTO req)
        {
            UserAuthDTO credentials =
                new() { EmailAddress = req.EmailAddress, Username = req.Username };

            bool userExists = await UserExists(credentials);

            if (!userExists)
            {
                User newUser =
                    new()
                    {
                        ID = GenerateNewID(),
                        FirstName = req.FirstName!,
                        MiddleName = req.MiddleName,
                        LastName = req.LastName,
                        EmailAddress = req.EmailAddress!,
                        Username = req.Username!,
                        BillingAddress = req.BillingAddress!,
                        DeliveryAddress = req.DeliveryAddress!,
                        RoleID = 1,
                        AddedBy = 0,
                        AddedOn = DateTime.Now,
                        IsDeleted = 0,
                        HashedPassword = BCrypt.Net.BCrypt.HashPassword(req.Password)
                    };

                _ = await _context.Users.AddAsync(newUser);
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UserExists(UserAuthDTO req)
        {
            var usernameExists = await _context.Users.AnyAsync(u => u.Username == req.Username);
            var emailExists = await _context.Users.AnyAsync(
                u => u.EmailAddress == req.EmailAddress
            );
            return usernameExists || emailExists;
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

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            List<UserDTO> userList = await _context.Users
                .Select(
                    user =>
                        new UserDTO
                        {
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            BillingAddress = user.BillingAddress,
                            DeliveryAddress = user.DeliveryAddress,
                            // TODO: PHONENUMBER
                            EmailAddress = user.EmailAddress,
                            Username = user.Username,
                            RoleID = 1
                        }
                )
                .ToListAsync();
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

        public async Task<int> GetUserID(string username)
        {
            User? user = await GetUserByUsername(username);
            int ID = user is not null ? user.ID : 0;
            return ID;
        }

        public async Task<bool> UpdateUser(UserDTO req)
        {
            UserAuthDTO credentials =
                new() { EmailAddress = req.EmailAddress, Username = req.Username };

            bool userExists = await UserExists(credentials);

            if (userExists)
            {
                var existingUser = credentials.EmailAddress is not null
                    ? await GetUserByEmail(credentials.EmailAddress)
                    : await GetUserByUsername(credentials.Username!);

                var DTOprops = typeof(UserDTO).GetProperties();

                foreach (var prop in DTOprops)
                {
                    if (
                        typeof(User).GetProperty(prop.Name) != null
                        && prop.GetValue(req) != null
                        && prop.Name != nameof(UserDTO.Numbers)
                    )
                    {
                        typeof(User)
                            .GetProperty(prop.Name)
                            ?.SetValue(existingUser, prop.GetValue(req));
                    }
                }
                await _context.SaveChangesAsync();
            }
            return false;
        }
    }
}
