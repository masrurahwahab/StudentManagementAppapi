namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateClassSubjectDto
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }

       
        public int PeriodsPerWeek { get; set; } = 5;
    }

}
