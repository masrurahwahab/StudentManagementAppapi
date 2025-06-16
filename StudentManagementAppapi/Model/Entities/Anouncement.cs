using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Model.Entities
{
    public class Announcement : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;

        [Required]
        public TargetAudience TargetAudience { get; set; }

        public Guid? ClassId { get; set; }
        public Class? Class { get; set; }

        [Required]
        public Priority Priority { get; set; } = Priority.Normal;

        public DateTime? ExpireDate { get; set; }

        public DateTime? StartDate { get; set; }

    }

}
