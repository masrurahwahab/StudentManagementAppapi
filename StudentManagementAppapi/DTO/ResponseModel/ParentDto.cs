using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ParentDto
    {
        public Guid Id { get; set; }
        public UserResponseModel User { get; set; } = null!;
        public string Occupation { get; set; } = string.Empty;
        public string Phone { get; set; } 
        public RelationshipType RelationshipToStudent { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
