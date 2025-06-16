using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IStudentService
    {
        Task<ResponseWrapper<StudentDto>> CreateStudentAsync(CreateStudent createStudentDto);
        Task<ResponseWrapper<StudentDto>> GetStudentByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<StudentDto>>> GetAllStudentsAsync();
        Task<ResponseWrapper<PagedResponse<StudentDto>>> GetPagedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null);
        Task<ResponseWrapper<IEnumerable<StudentDto>>> GetStudentsByClassAsync(Guid classId);
        Task<ResponseWrapper<StudentDto>> UpdateStudentAsync(Guid id, CreateStudent updateDto);
        Task<ResponseWrapper<bool>> DeleteStudentAsync(Guid id);
        Task<ResponseWrapper<StudentDto>> GetByAdmissionNumberAsync(string admissionNumber);
    }
}
