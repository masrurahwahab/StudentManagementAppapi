using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Teacher : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string EmployeeId { get; set; } = string.Empty;

        
        public List<string> SubjectsTaught { get; set; } = new List<string>();
       

        public bool IsClassTeacher { get; set; } = false;

        public Guid? ClassId { get; set; }
        public Class? Class { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Qualification { get; set; }

        
        [StringLength(50)]
        public string? Department { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } = "Active";

        public decimal? Salary { get; set; }

        public DateTime? TerminationDate { get; set; }

       
        public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
        public ICollection<Attendance> AttendanceRecords { get; set; } = new List<Attendance>();
        public ICollection<Disciplinary> ReportedDisciplinaryActions { get; set; } = new List<Disciplinary>();
    }

}
