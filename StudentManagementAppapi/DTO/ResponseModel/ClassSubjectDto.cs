namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ClassSubjectDto
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }

        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public int PeriodsPerWeek { get; set; }
    }

}
