using contactManagerAPI.DTO;

namespace contactManagerAPI.Services.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByID(int UserID);
        Task<bool> CreateUser(UserDTO req);
        Task<bool> UserExists(UserAuthDTO req);
        Task<bool> DeleteUserByUsername(string username);
        Task<bool> DeleteUserByID(int ID);
        Task<bool> UpdateUser(UserDTO req);
        Task<int> GetUserID(string username);
        Task<IEnumerable<UserDTO>> GetAllUsers();
    }
}
