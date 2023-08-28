using System.Security.Cryptography.X509Certificates;
using BCrypt.Net;
using contactManagerAPI.DTO;
using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Services.MiscRepository;
using contactManagerAPI.Services.UserRepository;
using Serilog;

namespace contactManagerAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditor;
        private readonly IMiscRepository _numberRepo;

        public AuthService(
            IUserRepository userRepository,
            IAuditService auditService,
            IMiscRepository numberRepo
        )
        {
            _userRepository = userRepository;
            _auditor = auditService;
            _numberRepo = numberRepo;
        }

        public async Task<SvcResponse<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var Users = await _userRepository.GetAllUsers();
            foreach (var user in Users)
            {
                user.Numbers = await _numberRepo.GetContactNumbers(OwnerID: user.ID);
            }
            var res = new SvcResponse<IEnumerable<UserDTO>>
            {
                Data = Users,
                Error = Users is null ? "No users in database" : "",
                Message = "200"
            };
            return res;
        }

        public async Task<SvcResponse<int>> AuthenticateUser(UserAuthDTO req)
        {
            var res = new SvcResponse<int>();
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
            else if (BCrypt.Net.BCrypt.Verify(req.Password, userData.HashedPassword))
            {
                res.Data = userData.ID;
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

                foreach (var number in req.Numbers)
                {
                    number.OwnerID = entityID;
                    number.OwnerType = "User";
                    bool numsAdded = await _numberRepo.AddContactNumber(number);
                    if (!numsAdded)
                        Log.Error($"userRequest id={entityID} unable to add number={number}");
                }

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
            User? userData = await _userRepository.GetUserByUsername(req.Username!);
            if (userData != null && BCrypt.Net.BCrypt.Verify(userData.HashedPassword, req.Password))
            {
                res.Data =
                    "User has been deactivated. Account will no longer be accessible in 30 days.";
                res.Message = "200";
                _ = _userRepository.DeleteUserByUsername(req.Username!);
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
                if (req.Numbers != null && req.Numbers.Any())
                {
                    foreach (var number in req.Numbers!)
                    {
                        // GET USER ID
                        // SET NUMBER OWNER ID AS USER ID
                        number.OwnerType = "User";
                        bool numUpdated = await _numberRepo.UpdateContactNumber(number);
                        if (!numUpdated)
                            Log.Error(
                                $"unable to update number ID={number.ID} user={req.Username}"
                            );
                    }
                }
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

        public async Task<SvcResponse<string>> UserAlreadyExists(UserAuthDTO req)
        {
            var res = new SvcResponse<string>();
            var userExists = await _userRepository.UserExists(req);
            if (userExists)
            {
                res.Success = false;
                res.Error = "Bad Request";
                res.Message = "400";
                res.Data = "User Already Exists";
            }
            else
            {
                res.Success = true;
                res.Message = "200";
            }
            return res;
        }
    }
}
