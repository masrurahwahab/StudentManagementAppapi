using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsCore { get; set; }
        public ClassLevel ClassLevel { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
