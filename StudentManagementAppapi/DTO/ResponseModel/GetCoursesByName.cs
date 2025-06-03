using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class GetCoursesByName
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Class { get; set; }
    }
}
