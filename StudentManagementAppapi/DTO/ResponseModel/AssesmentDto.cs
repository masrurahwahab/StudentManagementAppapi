using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class AssessmentDto
    {
        public Guid Id { get; set; }
        public Guid ClassSubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public Guid TermId { get; set; }
        public string TermName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public AssessmentType AssessmentType { get; set; }
        public decimal MaxScore { get; set; }
        public DateTime DateGiven { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
