using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IAcademicTermService
    {
        Task<ResponseWrapper<AcademicTermDto>> CreateTermAsync(CreateAcademicTermDto createTermDto);
        Task<ResponseWrapper<AcademicTermDto>> GetTermByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<AcademicTermDto>>> GetAllTermsAsync();
        Task<ResponseWrapper<AcademicTermDto>> UpdateTermAsync(Guid id, CreateAcademicTermDto updateDto);
        Task<ResponseWrapper<bool>> DeleteTermAsync(Guid id);
        Task<ResponseWrapper<AcademicTermDto>> GetCurrentTermAsync();
        Task<ResponseWrapper<bool>> SetCurrentTermAsync(Guid termId);
    }
}
