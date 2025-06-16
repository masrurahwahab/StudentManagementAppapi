namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateDisciplinaryDto
    {
        public Guid StudentId { get; set; }
        public string Offense { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
        public Guid ReportedById { get; set; }
        public DateTime DateReported { get; set; }
    }
}
