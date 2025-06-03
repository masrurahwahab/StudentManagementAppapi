using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class SchoolClassRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Section { get; set; }
    }

}
