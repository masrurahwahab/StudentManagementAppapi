namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class SchoolClassResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Section { get; set; }
        public int StudentCount { get; set; }
        public int CourseCount { get; set; }
    }

}
