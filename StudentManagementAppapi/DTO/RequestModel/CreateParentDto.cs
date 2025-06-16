using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.DTO.RequestModel
{
    public class CreateParentDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;        
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public RelationshipType RelationshipToStudent { get; set; }
        public Guid StudentId { get; set; }
    }

}
