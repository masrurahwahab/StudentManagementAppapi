using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ICoursesRepository
    {
        Course CreateCourse(Course course);
        void DeleteCourse(string courseName);
        Course UpdateCourse(Course course);
        List<Course> GetAllCourse(Func<Course, bool> expression);
        bool IsExist(Func<Course, bool> expression);
        int SaveChanges();
        Course Get(Func<Course, bool> expression);

    }
}
