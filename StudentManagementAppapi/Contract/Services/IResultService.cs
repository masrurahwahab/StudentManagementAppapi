using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IResultService
    {
        Task<ResponseWrapper<ResultResponse>> CreateResultAsync(CreateResultDto createResultDto);
        Task<ResponseWrapper<IEnumerable<ResultResponse>>> GetResultsByStudentIdAsync(string admissionNo);
        Task<ResponseWrapper<IEnumerable<ResultResponse>>> GetResultsByAssessmentIdAsync(Guid assessmentId);
        Task<ResponseWrapper<ResultResponse>> UpdateResultAsync(Guid id, CreateResultDto updateDto);
        Task<ResponseWrapper<bool>> DeleteResultAsync(Guid id);
    }

}
