using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Model.Entities
{
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; } = GenerateCode();
        public Guid InstructorId { get; set; }
        public Guid ClassId { get; set; }

        public SchoolClass SchoolClass { get; set; }

        public static string GenerateCode()
        {
            Random random = new Random();
            return $"BC{random.Next(1000, 9999)}";
        }
    }


}
