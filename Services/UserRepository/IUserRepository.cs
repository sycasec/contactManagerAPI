using contactManagerAPI.DTO;

namespace contactManagerAPI.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<bool> CreateUser(UserDTO req);
        Task<bool> DeleteUserByUsername(string username);
        Task<bool> DeleteUserByID(int ID);
        Task<bool> UpdateUser(UserDTO req);
        Task<bool> UpdateUserByID(int ID, User user);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
