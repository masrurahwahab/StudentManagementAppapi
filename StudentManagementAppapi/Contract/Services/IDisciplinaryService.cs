using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IDisciplinaryService
    {
        Task<ResponseWrapper<DisciplinaryDto>> CreateDisciplinaryRecordAsync(CreateDisciplinaryDto createDisciplinaryDto);
        Task<ResponseWrapper<DisciplinaryDto>> GetDisciplinaryRecordByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetAllDisciplinaryRecordsAsync();
        Task<ResponseWrapper<DisciplinaryDto>> UpdateDisciplinaryRecordAsync(Guid id, CreateDisciplinaryDto updateDto);
        Task<ResponseWrapper<bool>> DeleteDisciplinaryRecordAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetDisciplinaryRecordsByStudentAsync(Guid studentId);
        Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetDisciplinaryRecordsByStatusAsync(DisciplinaryStatus status);
        Task<ResponseWrapper<bool>> UpdateDisciplinaryStatusAsync(Guid id, DisciplinaryStatus status);
    }
}
