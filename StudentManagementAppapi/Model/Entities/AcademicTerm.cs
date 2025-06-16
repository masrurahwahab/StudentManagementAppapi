using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class AcademicTerm : BaseEntity
    {
        [Required]
        public TermName Name { get; set; }

        [Required]
        [StringLength(20)]
        public string AcademicYear { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; } = false;

        
        public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public ICollection<TermReport> TermReports { get; set; } = new List<TermReport>();
    }
}
