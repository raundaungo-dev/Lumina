using System.ComponentModel.DataAnnotations;

namespace LuminaPortal.Models
{
    public class UserAccount
    {
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        public string FullName { get; set; }
        [Required, DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int RoleID { get; set; }
    }
}