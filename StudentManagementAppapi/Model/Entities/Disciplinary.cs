using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Disciplinary : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Offense { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string ActionTaken { get; set; } = string.Empty;

        public Guid ReportedById { get; set; }
        public Teacher ReportedBy { get; set; } = null!;

        [Required]
        public DateTime DateReported { get; set; }

        [Required]
        public DisciplinaryStatus Status { get; set; } = DisciplinaryStatus.Pending;
    }
}
