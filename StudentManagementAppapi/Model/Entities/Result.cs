using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Result : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid AssessmentId { get; set; }
        public Assessment Assessment { get; set; } = null!;

        [Required]
        public decimal Score { get; set; }

        //[Required]
        //[StringLength(2)]
        //public string Grade { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Remarks { get; set; }

        [Required]
        public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
    }

}
