using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace contactManagerAPI.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public int RoleID { get; set; } = 1;
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int IsDeleted { get; set; }
    }
}
