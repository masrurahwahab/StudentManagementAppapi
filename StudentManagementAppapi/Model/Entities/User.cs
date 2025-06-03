using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Model.Entities
{
    public class User : BaseEntity
    {
        public Role Role { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordHash { get; set; } 
        public string PasswordSalt { get; set; } 
    }

}
