namespace StudentManagementAppapi.DTO.RequestModel
{
    public class UpdateCourseRequestModel
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public int MyProperty { get; set; }
    }
}
