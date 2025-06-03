using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface ICourseRegistrationService
    {
        ResponseWrapperl<Guid> EnrollForACourse(Enrollforacourse enrollforacourse);
       
        ResponseWrapperl<IEnumerable<ViewAllEnrolledCourses>> GetAllEnrollCourses();
        ResponseWrapperl<ViewAllEnrolledCourses> GetEnrollCourse(string coursename);
    }
}
