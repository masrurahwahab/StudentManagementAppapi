using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateTermReportDto
    {
        public Guid StudentId { get; set; }
        public Guid TermId { get; set; }
        public string admissionNo { get; set; }
        public string? ClassTeacherComment { get; set; }
        public string? PrincipalComment { get; set; }
        public DateTime? NextTermBegins { get; set; }
    }
}
