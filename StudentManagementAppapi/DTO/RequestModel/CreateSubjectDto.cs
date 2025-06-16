using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsCore { get; set; }
        public ClassLevel ClassLevel { get; set; }
    }

}
