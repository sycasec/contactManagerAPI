namespace contactManagerAPI.AuthServices
{
    public interface IAuthService
    {
        Task<SvcResponse<string>> AuthenticateUser(string username, string password);
        Task<SvcResponse<string>> CreateUser(User newUser);
        Task<SvcResponse<string>> DeleteUserByUsername(string username);
        Task<SvcResponse<string>> UpdateUserByUsername(string username);

    }
}
