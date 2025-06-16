namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public UserResponseModel User { get; set; } = null!;
        public string EmployeeId { get; set; } = string.Empty;
        public List<string> SubjectsTaught { get; set; } = new List<string>();
        public bool IsClassTeacher { get; set; }
        public Guid? ClassId { get; set; }
        public string Phone { get; set; }
        public string? ClassName { get; set; }
        public DateTime HireDate { get; set; }
        public string Qualification { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }

}
