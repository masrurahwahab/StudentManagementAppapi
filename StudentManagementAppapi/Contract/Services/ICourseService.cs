using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Services
{
    public interface ICourseService
    {
        ResponseWrapperl<Guid> AddNewCourse(CreateCourseRequestModel createCourseRequestModel);

        ResponseWrapperl<IEnumerable<Course>> GetAllCourse();
        ResponseWrapperl<Course> GetCourseByName(string coursename);
        ResponseWrapperl<Guid> DeleteCourse(string coursename);
        ResponseWrapperl<ViewAllCourses> UpdateCourse(CreateCourseRequestModel createCourseRequestModel);
   
    }
}
