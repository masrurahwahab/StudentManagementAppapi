namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateResultDto
    {
        public Guid StudentId { get; set; }
        public Guid AssessmentId { get; set; }
        public decimal Score { get; set; }
        public string? Remarks { get; set; }
       
    }
}
