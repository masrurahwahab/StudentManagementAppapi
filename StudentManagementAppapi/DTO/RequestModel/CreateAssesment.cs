using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateAssessmentDto
    {
        public Guid ClassSubjectId { get; set; }
        public Guid TermId { get; set; }
        public string Title { get; set; } = string.Empty;
        public AssessmentType AssessmentType { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime? DueDate { get; set; }
    }

}
