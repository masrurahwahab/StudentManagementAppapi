using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateAnnouncementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public TargetAudience TargetAudience { get; set; }
        public Guid AuthorId { get; set; }
       
        public Guid? ClassId { get; set; }
        public Priority Priority { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
