using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class TermReportDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Guid TermId { get; set; }
        public TermName TermName { get; set; }
        public decimal TotalScore { get; set; }
        public decimal AverageScore { get; set; }
        public int PositionInClass { get; set; }
        public string ClassTeacherComment { get; set; } = string.Empty;
        public string PrincipalComment { get; set; } = string.Empty;
        public DateTime NextTermBegins { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateTime TermEnd { get; set; }
    }

}
