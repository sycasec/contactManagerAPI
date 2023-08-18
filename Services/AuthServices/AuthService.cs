using System.Security.Cryptography.X509Certificates;
using BCrypt.Net;
using contactManagerAPI.DTO;
using contactManagerAPI.UserRepository;

namespace contactManagerAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SvcResponse<string>> AuthenticateUser(UserAuthDTO req)
        {
            var res = new SvcResponse<string>();
            User? userData;
            if (req.Username == "")
            {
                userData = await _userRepository.GetUserByEmail(req.EmailAddress);
            }
            else
            {
                userData = await _userRepository.GetUserByUsername(req.Username);
            }

            if (userData == null || !BCrypt.Net.BCrypt.Verify(req.Password, userData.HashedPassword))
            {
                res.Success = false;
                res.Error = "Bad Request";
                res.Message = "User does not exist or wrong password was entered.";
                res.Data = "User does not exist or wrong password was entered.";
            }
            else if (userData != null && BCrypt.Net.BCrypt.Verify(req.Password, userData.HashedPassword))
            {
                res.Message = "User login success!";
                res.Data = "User login success!";
            }
            return res;
        }

        public async Task<SvcResponse<string>> CreateUser(UserDTO req)
        {
            var taskSuccess = await _userRepository.CreateUser(req);
            var res = new SvcResponse<string>();
            if (taskSuccess)
            {
                res.Data = "User created successfully!";
                res.Message = "User created successfully!";
            }

            return res;
        }

        public async Task<SvcResponse<string>> DeactivateUser(UserAuthDTO req)
        {
            var res = new SvcResponse<string>();
            User? userData = await _userRepository.GetUserByUsername(req.Username);
            if (BCrypt.Net.BCrypt.Verify(userData.HashedPassword, req.Password))
            {
                res.Data = "User has been deactivated. Account will no longer be accessible in 30 days.";
                res.Message = "User has been deactivated. Account will no longer be accessible in 30 days.";
                _ = _userRepository.DeleteUserByUsername(req.Username);
            }
            else
            {
                res.Success = false;
                res.Error = "Bad Request";
                res.Message = "Wrong password was entered.";
                res.Data = "Wrong password was entered.";
            }
            return res;
        }

        public async Task<SvcResponse<string>> UpdateUser(UserDTO req)
        {
            var res = new SvcResponse<string>();
            var taskSuccess = await _userRepository.UpdateUser(req);
            if (taskSuccess)
            {
                res.Data = "Update success!";
                res.Message = "Update success!";
            }
            return res;
        }

    }
}
