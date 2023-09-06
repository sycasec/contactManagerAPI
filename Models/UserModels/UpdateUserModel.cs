using System.ComponentModel.DataAnnotations;

namespace contactManagerAPI.Models.UserModels
{
    /// <summary>
    /// Model for updating user profile information.
    /// </summary>
    public class UpdateUserModel
    {
        /// <summary>
        /// Gets or sets the updated first name of the user.
        /// </summary>
        [RegularExpression(@"^[\w\d\s]+$", ErrorMessage = "Invalid first name.")]
        [StringLength(
            40,
            ErrorMessage = "First Name must be between {2} and {1} characters long.",
            MinimumLength = 2
        )]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated last name of the user.
        /// </summary>
        [RegularExpression(@"^[\w\d\s]+$", ErrorMessage = "Invalid last name.")]
        [StringLength(
            40,
            ErrorMessage = "Last Name must be between {2} and {1} characters long.",
            MinimumLength = 2
        )]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated username of the user.
        /// </summary>
        [StringLength(
            40,
            ErrorMessage = "Username must be between {2} and {1} characters long.",
            MinimumLength = 2
        )]
        public string Username { get; set; } = string.Empty;
    }
}
