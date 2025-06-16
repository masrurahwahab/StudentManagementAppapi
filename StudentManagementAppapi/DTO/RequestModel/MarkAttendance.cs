using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class MarkAttendanceDto
    {
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public string? Remarks { get; set; }
    }
}
