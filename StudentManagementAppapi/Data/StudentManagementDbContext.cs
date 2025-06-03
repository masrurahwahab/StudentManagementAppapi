using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Data
{
  public class StudentManagementDbContext : DbContext
    {
        public StudentManagementDbContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
    }
}
