using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace contactManagerAPI.Models.AuthModels {
    public class LoginModel {
        [Required(ErrorMessage = "Email Address is a required field.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email Address must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string EmailAddress { get; set; } = string.Empty;


        [PasswordPropertyText]
        [Required(ErrorMessage = "Password is a required field.")]
        [StringLength(150, ErrorMessage = "Password must be between {2} and {1} characters long", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
