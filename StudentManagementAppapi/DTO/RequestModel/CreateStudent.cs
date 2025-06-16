using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateStudent
    {
        public CreateUserRequestModel User { get; set; } = new(); 
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? ParentPhone { get; set; }
        public string? ParentEmail { get; set; }
        public string? EmergencyContact { get; set; }
        public Guid ClassId { get; set; }
        public DateTime AdmissionDate { get; set; }
        
    }
}
