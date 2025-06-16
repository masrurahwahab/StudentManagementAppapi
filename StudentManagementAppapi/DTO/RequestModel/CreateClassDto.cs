using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{   
    public class CreateClassDto
    {
        public Guid ClassId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ClassLevel Level { get; set; }
        public Guid? ClassTeacherId { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public int MaxStudents { get; set; } = 40;
    }

}
