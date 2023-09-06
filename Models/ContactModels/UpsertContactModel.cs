using System.ComponentModel.DataAnnotations;

namespace contactManagerAPI.Models.ContactModels
{
    /// <summary>
    /// Model for updating or inserting user contact information.
    /// </summary>
    public class UpsertContactModel
    {
        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        [Required(ErrorMessage = "First Name field is required.")]
        [RegularExpression(@"^[\w\d\s]+$", ErrorMessage = "Invalid first name.")]
        [StringLength(
            40,
            ErrorMessage = "First Name must be between {2} and {1} characters long.",
            MinimumLength = 2
        )]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        [Required(ErrorMessage = "Last Name field is required.")]
        [RegularExpression(@"^[\w\d\s]+$", ErrorMessage = "Invalid last name.")]
        [StringLength(
            40,
            ErrorMessage = "Last Name must be between {2} and {1} characters long.",
            MinimumLength = 2
        )]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the contact.
        /// </summary>
        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email address.")]
        [StringLength(
            100,
            ErrorMessage = "Email Address must be between {2} and {1} characters long.",
            MinimumLength = 3
        )]
        public string EmailAddress { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? IsFavorite { get; set; }

        public int? IsBlocked { get; set; }
    }
}
