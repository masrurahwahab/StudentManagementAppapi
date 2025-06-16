using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{

    public interface ITermReportService
    {
        Task<ResponseWrapper<TermReportDto>> CreateTermReportAsync(CreateTermReportDto createTermReportDto);
        Task<ResponseWrapper<TermReportDto>> GetTermReportByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetAllTermReportsAsync();
        Task<ResponseWrapper<TermReportDto>> UpdateTermReportAsync(Guid id, CreateTermReportDto updateDto);
        Task<ResponseWrapper<bool>> DeleteTermReportAsync(Guid id);
        Task<ResponseWrapper<TermReportDto>> GetTermReportByStudentAndTermAsync(string admissionNo, Guid termId);
        Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByStudentAsync(string admissionNo);
        Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByTermAsync(Guid termId);
        Task<ResponseWrapper<IEnumerable<TermReportDto>>> GetTermReportsByClassAsync(Guid classId, Guid termId);
    }
}
