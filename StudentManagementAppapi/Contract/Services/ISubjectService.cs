using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Contract.Services
{
    public interface ISubjectService
    {
        Task<ResponseWrapper<SubjectDto>> CreateSubjectAsync(CreateSubjectDto createSubjectDto);
        Task<ResponseWrapper<SubjectDto>> GetSubjectByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<SubjectDto>>> GetAllSubjectsAsync();
        Task<ResponseWrapper<SubjectDto>> UpdateSubjectAsync(Guid id, CreateSubjectDto updateDto);
        Task<ResponseWrapper<bool>> DeleteSubjectAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<SubjectDto>>> GetSubjectsByClassLevelAsync(ClassLevel classLevel);
    }
}
