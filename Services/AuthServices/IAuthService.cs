using contactManagerAPI.Models.AuthModels;
using contactManagerAPI.Models.UserModels;

namespace contactManagerAPI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<bool> UserAlreadyExists(UserDetailsModel existRequest);
        Task<string> UserLogin(LoginModel loginRequest);
        Task<string> UserRegister(RegisterModel registerRequest);
    }
}
