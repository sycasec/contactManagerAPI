using contactManagerAPI.Entities;
using contactManagerAPI.Models.AuthModels;
using contactManagerAPI.Models.UserModels;

namespace contactManagerAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> DeactivateUser(LoginModel deactivateRequest);
        Task<UserDetailsModel> UpdateUser(UpdateUserModel updateRequest);
        Task<ICollection<UserDetailsModel>> GetAllUsers();
        Task<UserDetailsModel> GetUserDetails();
        Task<User> GetUserByToken();
    }
}
