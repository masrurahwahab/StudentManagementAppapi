using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface ITeacherService
    {
        Task<ResponseWrapper<TeacherDto>> CreateTeacherAsync(CreateTeacherDto createTeacherDto);
        Task<ResponseWrapper<TeacherDto>> GetTeacherByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<TeacherDto>>> GetAllTeachersAsync();
        Task<ResponseWrapper<TeacherDto>> UpdateTeacherAsync(Guid id, UpdateTeacherDto updateDto);
        Task<ResponseWrapper<bool>> DeleteTeacherAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<TeacherDto>>> GetTeachersBySubjectAsync(string subject);
    }
}
