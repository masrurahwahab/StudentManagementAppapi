using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Model.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

}
