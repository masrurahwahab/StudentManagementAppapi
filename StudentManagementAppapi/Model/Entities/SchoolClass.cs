namespace StudentManagementAppapi.Model.Entities
{
    public class SchoolClass : BaseEntity
    {
        public string Name { get; set; } 
        public string Section { get; set; } 
        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

}
