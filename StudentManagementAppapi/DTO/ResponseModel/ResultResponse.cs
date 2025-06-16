using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ResultResponse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Guid AssessmentId { get; set; }
        public AssessmentType AssessmentTitle { get; set; } 
        public string SubjectName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        
        public string? Remarks { get; set; }
        public DateTime RecordedDate { get; set; }
    }

}
