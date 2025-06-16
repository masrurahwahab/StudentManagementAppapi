using MySqlX.XDevAPI.Common;
using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Student : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string AdmissionNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(15)]
        public string? ParentPhone { get; set; }

        [StringLength(100)]
        public string? ParentEmail { get; set; }

        [StringLength(100)]
        public string? EmergencyContact { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; } = default!;

        [Required]
        public DateTime AdmissionDate { get; set; }

        public StudentStatus Status { get; set; } = StudentStatus.Active;


       
        public ICollection<Result> Results { get; set; } = new List<Result>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Disciplinary> DisciplinaryRecords { get; set; } = new List<Disciplinary>();
        public ICollection<Parent> Parents { get; set; } = new List<Parent>();


        //public static string GenerateAdmmissionNo()
        //{
        //    Random random = new Random();
        //    return $"BC{random.Next(1000, 9999)}";
        //}
    }

}
