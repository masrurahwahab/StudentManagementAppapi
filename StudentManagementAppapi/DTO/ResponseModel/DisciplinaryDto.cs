using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class DisciplinaryDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Offense { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
        public Guid ReportedById { get; set; }
        public string ReportedByName { get; set; } = string.Empty;
        public DateTime DateReported { get; set; }
        public DisciplinaryStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
