using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        //Task<ResponseWrapper<UserResponseModel>> GetCurrentUserAsync(Guid userId);
        //Task<ResponseWrapper<UserResponseModel>> RegisterUserAsync(CreateUserRequestModel createUserDto);
    }
}
