using AutoMapper;
using contactManagerAPI.Entities;
using contactManagerAPI.Models.UserModels;

namespace contactManagerAPI.Mappers
{
    /// <summary>
    /// Mapper class to define mappings between User entities and user-related models.
    /// </summary>
    public class UserMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper"/> class.
        /// </summary>
        public UserMapper()
        {
            // Map from User entity to GetUserProfileModel and vice versa
            CreateMap<User, UserDetailsModel>()
                .ReverseMap();

            // Map from User entity to UpdateUserProfileModel and vice versa
            CreateMap<User, UpdateUserModel>()
                .ReverseMap();

            // Map from User entity to UpdateUserPasswordModel and vice versa
            CreateMap<User, SecurityUpdateModel>()
                .ReverseMap();
        }
    }
}
