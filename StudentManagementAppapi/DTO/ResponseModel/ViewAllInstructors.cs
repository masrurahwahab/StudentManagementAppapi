using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ViewAllInstructors
    {
        public List<InstructorDto> AllInstructors { get; set; }
    }

    public class InstructorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

}
