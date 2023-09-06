using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace contactManagerAPI.Models.AuthModels {
    public class RegisterModel {
        [Required(ErrorMessage = "First Name is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the First Name is up to 40 characters.")]
        [MinLength(1, ErrorMessage = "At least one (1) character may be accepted for the First Name.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the Last Name is up to 40 characters.")]
        [MinLength(1, ErrorMessage = "At least one (1) character may be accepted for the Last Name.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is a required field.")]
        [MaxLength(40, ErrorMessage = "Maximum length for the Username is up to 40 characters.")]
        [MinLength(1, ErrorMessage = "At least one (1) character may be accepted for the Username.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email address.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Email field is 100 characters.")]
        [MinLength(3, ErrorMessage = "Minimum length for the Email field is 3 characters.")]
        public string EmailAddress { get; set; } = string.Empty;

        [PasswordPropertyText]
        [Required(ErrorMessage = "Password field is required.")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password. Password must be Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Password field is 150 characters.")]
        [MinLength(6, ErrorMessage = "Minimum length for the Password field is 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [PasswordPropertyText]
        [Required(ErrorMessage = "Confirm Password field is required.")]
        [Compare("Password", ErrorMessage = "Confirm Password does not match with Password.")]
        [MaxLength(150, ErrorMessage = "Maximum length for the Confirm Password field is 150 characters.")]
        [MinLength(6, ErrorMessage = "Minimum length for the Confirm Password field is 6 characters.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
