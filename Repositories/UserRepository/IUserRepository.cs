using contactManagerAPI.Entities;

namespace contactManagerAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByID(int UserID);
        Task<User?> GetUserByToken(int UserID, string EmailAddress);
        Task<bool> CreateUser(User newUser);
        Task<bool> UserExists(User user);
        Task<bool> DeleteUserByUsername(string username);
        Task<bool> DeleteUserByID(int ID);
        Task<bool> UpdateUser(User existingUser, User updateUser);
        Task<int> GetUserID(string username);
        Task<ICollection<User>> GetAllUsers();
    }
}
