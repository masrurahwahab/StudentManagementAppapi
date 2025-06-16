using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class TermReport : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid TermId { get; set; }
        public AcademicTerm Term { get; set; } = null!;

        
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total score must be non-negative")]
        public decimal TotalScore { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Average score must be between 0 and 100")]
        public decimal AverageScore { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Position must be greater than 0")]
        public int PositionInClass { get; set; }

        [StringLength(500)]
        public string? ClassTeacherComment { get; set; }

        [StringLength(500)]
        public string? PrincipalComment { get; set; }

        public DateTime? NextTermBegins { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        public int TotalStudentsInClass { get; set; }
        public string? Grade { get; set; }
        public bool IsPublished { get; set; } = false;
    }

   

}
