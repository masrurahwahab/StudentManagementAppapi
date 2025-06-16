using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Parent : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [StringLength(100)]
        public string? Occupation { get; set; }

        [Required]
        public RelationshipType RelationshipToStudent { get; set; }
        public string Phone { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
