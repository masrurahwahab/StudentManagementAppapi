using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Class : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ClassLevel Level { get; set; }

        public Guid? ClassTeacherId { get; set; }
        public Teacher? ClassTeacher { get; set; }

        [Required]
        [StringLength(20)]
        public string AcademicYear { get; set; } = string.Empty;

        public int MaxStudents { get; set; } = 40;

        public int CurrentStudentsCount { get; set; } = 0;

      
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
    }
}
