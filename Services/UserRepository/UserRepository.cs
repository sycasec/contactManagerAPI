using contactManagerAPI.DTO;
using contactManagerAPI.Models;

namespace contactManagerAPI.Services.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CreateUser(UserDTO req)
        {
            User request;
            request.ID =
        }
        public async Task<bool> DeleteUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteUserByID(int ID)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateUser(UserDTO req)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateUserByID(int ID, User user)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
