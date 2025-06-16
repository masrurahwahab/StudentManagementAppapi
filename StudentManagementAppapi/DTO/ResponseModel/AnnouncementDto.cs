using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public TargetAudience TargetAudience { get; set; }
        public Guid? ClassId { get; set; }
        public string? ClassName { get; set; }
        public Priority Priority { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
