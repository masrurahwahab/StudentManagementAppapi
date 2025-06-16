using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IClassService
    {
        Task<ResponseWrapper<Classresponse>> CreateClassAsync(CreateClassDto createClassDto);
        Task<ResponseWrapper<Classresponse>> GetClassByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<Classresponse>>> GetAllClassesAsync();
        Task<ResponseWrapper<IEnumerable<Classresponse>>> GetClassesByAcademicYearAsync(string academicYear);
        Task<ResponseWrapper<Classresponse>> UpdateClassAsync(Guid id, CreateClassDto updateDto);
        Task<ResponseWrapper<bool>> DeleteClassAsync(Guid id);
    }

}
