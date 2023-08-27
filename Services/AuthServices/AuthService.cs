using System.Security.Cryptography.X509Certificates;
using BCrypt.Net;
using contactManagerAPI.DTO;
using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Services.UserRepository;

namespace contactManagerAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditor;

        public AuthService(IUserRepository userRepository, IAuditService auditService)
        {
            _userRepository = userRepository;
            _auditor = auditService;
        }

        public async Task<SvcResponse<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var Users = await _userRepository.GetAllUsers();
            var res = new SvcResponse<IEnumerable<UserDTO>>
            {
                Data = Users,
                Error = Users is null ? "No users in database" : ""
            };
            return res;
        }

        public async Task<SvcResponse<string>> AuthenticateUser(UserAuthDTO req)
        {
            var res = new SvcResponse<string>();
            User? userData;

            _ = req.EmailAddress is not null
                ? userData = await _userRepository.GetUserByEmail(req.EmailAddress)
                : userData = await _userRepository.GetUserByUsername(req.Username!);

            if (
                userData == null || !BCrypt.Net.BCrypt.Verify(req.Password, userData.HashedPassword)
            )
            {
                res.Success = false;
                res.Error = "Bad Request";
                res.Message = "400";
                await _auditor.LogEvent(0, "Login", "User", "Login Failed. ID Unknown");
            }
            else if (
                userData != null && BCrypt.Net.BCrypt.Verify(req.Password, userData.HashedPassword)
            )
            {
                res.Message = "200";
                await _auditor.LogEvent(
                    userData.ID,
                    "Login",
                    "User",
                    $"Login Success, username={userData.Username}"
                );
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
                res.Message = "201";
                int entityID = await _userRepository.GetUserID(req.Username!);
                await _auditor.LogEvent(
                    entityID,
                    "CreateUser",
                    "User+System",
                    $"New User Created. username={req.Username}"
                );
            }
            else
            {
                res.Data = "User already exists!";
                res.Message = "400";
                res.Success = false;
                res.Error = "Bad Request";
            }
            return res;
        }

        public async Task<SvcResponse<string>> DeactivateUser(UserAuthDTO req)
        {
            var res = new SvcResponse<string>();
            User? userData = await _userRepository.GetUserByUsername(req.Username);
            if (BCrypt.Net.BCrypt.Verify(userData.HashedPassword, req.Password))
            {
                res.Data =
                    "User has been deactivated. Account will no longer be accessible in 30 days.";
                res.Message = "200";
                _ = _userRepository.DeleteUserByUsername(req.Username);
            }
            else
            {
                res.Success = false;
                res.Error = "Bad Request";
                res.Message = "400";
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
                res.Message = "201";
            }
            else
            {
                res.Success = false;
                res.Message = "400";
                res.Error = "Bad Request";
            }
            return res;
        }
    }
}
