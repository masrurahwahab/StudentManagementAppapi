namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateTeacherDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public List<string> SubjectsTaught { get; set; } = new List<string>();
        public bool IsClassTeacher { get; set; }
        //public Guid? ClassId { get; set; }
        public DateTime HireDate { get; set; }
        public string Qualification { get; set; } = string.Empty;
    }

    public class UpdateTeacherDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public List<string> SubjectsTaught { get; set; } = new List<string>();
        public bool IsClassTeacher { get; set; }
        public Guid? ClassId { get; set; }
        public DateTime HireDate { get; set; }
        public string Qualification { get; set; } = string.Empty;
    }
}
