using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class CourseRegistrationRepository : ICourseRegistrationRepository
    {
        private readonly StudentManagementDbContext _student;

        public CourseRegistrationRepository(StudentManagementDbContext studentManagementDbContext)
        {
            _student = studentManagementDbContext;
        }

        public CourseRegistration EnrolForCourse(CourseRegistration courseRegistration)
        {
            _student.CourseRegistrations.Add(courseRegistration);
            return courseRegistration;
        }

        public CourseRegistration Get(Func<CourseRegistration, bool> expression)
        {
            return _student.CourseRegistrations.FirstOrDefault(expression);
        }

        public List<CourseRegistration> GetAllCourseRegistration(Func<CourseRegistration, bool> expression)
        {
            return _student.CourseRegistrations.Where(expression)
                                               .Skip(1)
                                               .Take(15)
                                               .ToList();
        }

        public bool IsExist(Func<CourseRegistration, bool> expression)
        {
            return _student.CourseRegistrations.Any(expression);
        }

        public int SaveChanges()
        {
            return _student.SaveChanges(); 
        }
    }

}
