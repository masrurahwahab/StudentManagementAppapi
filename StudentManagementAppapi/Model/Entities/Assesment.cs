using MySqlX.XDevAPI.Common;
using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Assessment : BaseEntity
    {
        public Guid ClassSubjectId { get; set; }
        public ClassSubject ClassSubject { get; set; } = null!;

        public Guid TermId { get; set; }
        public AcademicTerm Term { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public AssessmentType AssessmentType { get; set; }

        [Required]
        public decimal MaxScore { get; set; }

        [Required]
        public DateTime DateGiven { get; set; }

        public DateTime? DueDate { get; set; }

        
        public ICollection<Result> Results { get; set; } = new List<Result>();
    }
}
