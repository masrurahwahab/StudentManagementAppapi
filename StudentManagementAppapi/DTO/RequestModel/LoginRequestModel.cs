using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
