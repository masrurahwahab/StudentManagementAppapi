using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateCourseRequestModel
    {
        public Guid CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string InstructorName { get; set; }
    }
}
