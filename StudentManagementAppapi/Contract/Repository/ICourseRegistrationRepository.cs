using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ICourseRegistrationRepository
    {
        CourseRegistration EnrolForCourse(CourseRegistration courseRegistration);
        bool IsExist(Func<CourseRegistration, bool> expression);
        int SaveChanges();
        CourseRegistration Get(Func<CourseRegistration, bool> expression);
        List<CourseRegistration> GetAllCourseRegistration(Func<CourseRegistration, bool> expression);
    }
}
