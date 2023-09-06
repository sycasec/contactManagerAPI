using contactManagerAPI.Entities;
using contactManagerAPI.Models.UserModels;
using contactManagerAPI.Repositories.UserRepository;
using contactManagerAPI.Services.AuditServices;
using AutoMapper;
using System.Security.Claims;
using contactManagerAPI.Models.AuthModels;

namespace contactManagerAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            IAuditService auditService,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepository =
                userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public async Task<ICollection<UserDetailsModel>> GetAllUsers()
        {
            var res = await _userRepository.GetAllUsers();
            return _mapper.Map<ICollection<UserDetailsModel>>(res);
        }

        public async Task<User> GetUserByToken()
        {
            var httpContext = _httpContext.HttpContext;

            if (httpContext is null)
            {
                throw new Exception("UserService.GetUserByToken() error: Token not found");
            }

            var user = await _userRepository.GetUserByToken(
                Convert.ToInt32(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                httpContext.User.FindFirstValue(ClaimTypes.Email)!
            );

            if (user is null)
            {
                throw new Exception("UserService.GetUserByToken() error: User is null - not found");
            }

            return user;
        }

        public async Task<bool> DeactivateUser(LoginModel deactivateRequest)
        {
            var userDetails = await GetUserByToken();

            if (!BCrypt.Net.BCrypt.Verify(deactivateRequest.Password, userDetails.HashedPassword))
            {
                throw new UnauthorizedAccessException("Password does not match.");
            }

            var response = await _userRepository.DeleteUserByID(userDetails.ID);
            if (!response)
            {
                throw new Exception("Failed to deactivate user!");
            }

            await _auditService.LogEvent(
                userDetails.ID,
                "UserService.DeactivateUser()",
                "User",
                $"User {userDetails.Username} successfully deactivated."
            );

            return response;
        }

        public async Task<UserDetailsModel> GetUserDetails()
        {
            var res = await GetUserByToken();

            await _auditService.LogEvent(
                res.ID,
                "UserService.GetUserDetails()",
                "User",
                $"Get User Profile - requested by user {res.Username} - Success"
            );

            return _mapper.Map<UserDetailsModel>(res);
        }

        public async Task<UserDetailsModel> UpdateUser(UpdateUserModel updateRequest)
        {
            var user = await GetUserByToken();
            User updateDetails = _mapper.Map<User>(updateRequest);
            var taskSuccess = await _userRepository.UpdateUser(user, updateDetails);

            if (!taskSuccess)
                throw new Exception($"Failed to udpate user {user.Username} at {DateTime.Now}");

            var res = _mapper.Map<UserDetailsModel>(updateRequest);
            res.ID = user.ID;
            res.EmailAddress = user.EmailAddress;

            await _auditService.LogEvent(
                res.ID,
                "UserService.UpdateUser()",
                "User",
                $"User {res.Username} updated successfully."
            );

            return res;
        }
    }
}
