namespace contactManagerAPI.Models.UserModels
{
    /// <summary>
    /// Model for retrieving user profile information.
    /// </summary>
    public class UserDetailsModel
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;
    }
}
