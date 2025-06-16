namespace StudentManagementAppapi.Model.Entities
{
    public class ClassSubject : BaseEntity
    {
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int PeriodsPerWeek { get; set; } = 5;

       
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
    }

}
