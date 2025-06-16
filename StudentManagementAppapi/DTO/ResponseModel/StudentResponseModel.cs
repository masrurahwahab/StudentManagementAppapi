using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public UserResponseModel User { get; set; } = new();
        public string AdmissionNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? ParentPhone { get; set; }
        public string? ParentEmail { get; set; }
        public string? EmergencyContact { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public StudentStatus Status { get; set; }
        public string? House { get; set; }
    }

}
