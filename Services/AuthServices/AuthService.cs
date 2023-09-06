using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.AuthModels;
using contactManagerAPI.Models.UserModels;
using contactManagerAPI.Repositories.UserRepository;
using contactManagerAPI.Services.AuditServices;
using contactManagerAPI.Utils;

namespace contactManagerAPI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(
            IUserRepository userRepository,
            IAuditService auditService,
            IMapper mapper,
            IConfiguration config
        )
        {
            _userRepository = userRepository;
            _auditService = auditService;
            _mapper = mapper;
            _config = config;
        }

        public async Task<string> UserLogin(LoginModel loginRequest)
        {
            var user = await _userRepository.GetUserByEmail(loginRequest.EmailAddress);

            if (
                user is null
                || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.HashedPassword)
            )
            {
                throw new Exception("User does not exist or password is incorrect.");
            }

            await _auditService.LogEvent(
                user.ID,
                "AuthService.UserLogin()",
                "User",
                $"User {user.Username} successfully logged in"
            );

            return JWTokenizer.GenerateToken(user, _config);
        }

        public async Task<string> UserRegister(RegisterModel registerRequest)
        {
            var newUserDetails = _mapper.Map<User>(registerRequest);
            var userExists = await _userRepository.UserExists(newUserDetails);
            if (userExists)
            {
                throw new Exception("User already exists.");
            }
            newUserDetails.HashedPassword = BCrypt.Net.BCrypt.HashPassword(
                registerRequest.Password
            );

            newUserDetails.AddedOn = DateTime.UtcNow;
            newUserDetails.AddedBy = 0;

            var userCreated = await _userRepository.CreateUser(newUserDetails);
            if (!userCreated)
            {
                throw new Exception("Failed to create new user.");
            }

            await _auditService.LogEvent(
                newUserDetails.ID,
                "AuthService.UserRegister()",
                "User",
                $"User {newUserDetails.Username} successfully created"
            );

            return JWTokenizer.GenerateToken(newUserDetails, _config);
        }

        public async Task<bool> UserAlreadyExists(UserDetailsModel existRequest)
        {
            var userDetails = _mapper.Map<User>(existRequest);
            return await _userRepository.UserExists(userDetails);
        }
    }
}
