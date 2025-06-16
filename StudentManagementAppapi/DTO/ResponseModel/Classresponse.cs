using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class Classresponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ClassLevel Level { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public string? ClassTeacherName { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public int CurrentStudentsCount { get; set; }
    }
}
