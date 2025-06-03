using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class CourseRepository : ICoursesRepository
    {
        private readonly StudentManagementDbContext _studentManagementDbContext;
        public CourseRepository(StudentManagementDbContext studentManagementDbContext)
        {
            _studentManagementDbContext = studentManagementDbContext;
        }

        public Course CreateCourse(Course course)
        {
            _studentManagementDbContext.Courses.Add(course);
            return course;
        }

        public void DeleteCourse(string courseName)
        {
            var delete = _studentManagementDbContext.Courses.FirstOrDefault(c => c.CourseName == courseName);
            _studentManagementDbContext.Remove(delete);

        }

        public Course Get(Func<Course, bool> expression)
        {
            return _studentManagementDbContext.Courses.FirstOrDefault(expression);
        }

        public List<Course> GetAllCourse(Func<Course, bool> expression)
        {
            var course = _studentManagementDbContext.Courses.Where(expression);
            var category = course.Skip(1).Take(15).ToList();
            return category;
        }

        public bool IsExist(Func<Course, bool> expression)
        {
            return _studentManagementDbContext.Courses.Any(expression);
        }

        public int SaveChanges()
        {
            return _studentManagementDbContext.SaveChanges();
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _studentManagementDbContext.Courses.Find(course.CourseName);
            if (existingCourse == null)
                return null;

            _studentManagementDbContext.Entry(existingCourse).CurrentValues.SetValues(course);
            return existingCourse;
        }
    }
}
