using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Model.Entities
{
    public class CourseRegistration : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }

}
