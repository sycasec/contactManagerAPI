namespace contactManagerAPI.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<bool> CreateUser(User newUser);
        Task<bool> DeleteUserByUsername(string username);
        Task<bool> DeleteUserByID(int ID);
        Task<bool> UpdateUserByUsername(string username, User user);
        Task<bool> UpdateUserByID(int ID, User user);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
