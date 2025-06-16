using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IAssessmentService
    {
        Task<ResponseWrapper<AssessmentDto>> CreateAssessmentAsync(CreateAssessmentDto createAssessmentDto);
        Task<ResponseWrapper<AssessmentDto>> GetAssessmentByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAllAssessmentsAsync();
        Task<ResponseWrapper<AssessmentDto>> UpdateAssessmentAsync(Guid id, CreateAssessmentDto updateDto);
        Task<ResponseWrapper<bool>> DeleteAssessmentAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAssessmentsByClassAsync(Guid classId);
    }

}
