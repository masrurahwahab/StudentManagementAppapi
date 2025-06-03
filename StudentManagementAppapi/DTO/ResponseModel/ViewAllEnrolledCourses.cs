using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ViewAllEnrolledCourses
    {
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string StudentName { get; set; }
        public DateTime DateRegistered { get; set; }

    }
}
