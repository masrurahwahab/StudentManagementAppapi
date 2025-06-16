using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateUserRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include an uppercase letter, a lowercase letter, and a number.")]
        public string Password { get; set; }
            

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }


        public string? ProfilePictureUrl { get; set; } 
    }
}
