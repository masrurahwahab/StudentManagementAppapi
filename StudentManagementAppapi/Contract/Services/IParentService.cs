using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IParentService
    {
        Task<ResponseWrapper<ParentDto>> CreateParentAsync(CreateParentDto createParentDto);
        Task<ResponseWrapper<ParentDto>> GetParentByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<ParentDto>>> GetAllParentsAsync();
        Task<ResponseWrapper<ParentDto>> UpdateParentAsync(Guid id, CreateParentDto updateDto);
        Task<ResponseWrapper<bool>> DeleteParentAsync(Guid id);
        Task<ResponseWrapper<ParentDto>> GetParentByStudentIdAsync(Guid studentId);
    }

}
