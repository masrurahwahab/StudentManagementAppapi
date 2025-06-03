namespace StudentManagementAppapi.Model.Entities
{
    public class Instructor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Course> Courses { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

}
