namespace contactManagerAPI.Models.ContactModels
{
    /// <summary>
    /// Model for retrieving user contact information.
    /// </summary>
    public class GetContactModel
    {
        /// <summary>
        /// Gets or sets the ID of the contact.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the contact.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the item is marked as favorite.
        /// </summary>
        public bool IsFavorite { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the item is blocked.
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        /// <summary>
        /// Gets or sets the time created of the contact
        /// </summary>
        public DateTime AddedOn { get; set; }
    }
}
