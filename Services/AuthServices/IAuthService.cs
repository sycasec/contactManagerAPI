using contactManagerAPI.DTO;

namespace contactManagerAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<SvcResponse<string>> AuthenticateUser(UserAuthDTO req);
        Task<SvcResponse<string>> CreateUser(UserDTO req);
        Task<SvcResponse<string>> DeactivateUser(UserAuthDTO req);
        Task<SvcResponse<string>> UpdateUser(UserDTO req);
        Task<SvcResponse<IEnumerable<UserDTO>>> GetAllUsers();
    }
}
