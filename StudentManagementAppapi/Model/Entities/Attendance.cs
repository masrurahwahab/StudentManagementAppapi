using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Attendance : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }

        [StringLength(200)]
        public string? Remarks { get; set; }

        public Guid MarkedById { get; set; }
        public Teacher MarkedBy { get; set; } = null!;
    }

}
