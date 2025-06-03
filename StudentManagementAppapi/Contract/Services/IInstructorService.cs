using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IInstructorService
    {
        ResponseWrapperl<Guid> AddNewInstructor(SiginRequestModel sigin);

        ResponseWrapperl<IEnumerable<ViewAllInstructors>> GetAllInstructor();
        ResponseWrapperl<InstructorResponsemodel> GetInstructorByName(string fullname);
        ResponseWrapperl<Guid> RemoveInstructor(string fullname);
        ResponseWrapperl<InstructorResponsemodel> UpdateInstructorProfile(UpdateInstructorProfile update);
    }
}
