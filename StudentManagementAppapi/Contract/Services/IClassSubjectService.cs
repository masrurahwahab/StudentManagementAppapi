using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IClassSubjectService
    {
        Task<ResponseWrapper<ClassSubjectDto>> GetByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetAllAsync();
        Task<ResponseWrapper<ClassSubjectDto>> CreateAsync(CreateClassSubjectDto dto);
        Task<ResponseWrapper<ClassSubjectDto>> UpdateAsync(Guid id, CreateClassSubjectDto dto);
        Task<ResponseWrapper<bool>> DeleteAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetByClassIdAsync(Guid classId);
        Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetBySubjectIdAsync(Guid subjectId);
        Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetByTeacherIdAsync(Guid teacherId);
        Task<ResponseWrapper<ClassSubjectDto>> GetByClassAndSubjectAsync(Guid classId, Guid subjectId);
    }

}
