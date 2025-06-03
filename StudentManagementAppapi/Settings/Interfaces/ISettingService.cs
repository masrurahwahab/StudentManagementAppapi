using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Settings.Interfaces
{
    public interface ISettingService
    {
        ResponseWrapperl<bool> ResetPassword(Guid userId, ForgetPassword model);
    }

}
