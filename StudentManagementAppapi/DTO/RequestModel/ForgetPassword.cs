using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class ForgetPassword
    {
        [Required]
        public string FormerPassword { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include an uppercase " +
        "letter, a lowercase letter, and a number.")]
        public string UpdatedPassword { get; set; }
    }
}
