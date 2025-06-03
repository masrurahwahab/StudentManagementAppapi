using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Settings.Interfaces
{
    public interface ISettingRepository
    {
        User ResetPassword(string formerPassword, string newPassword, Guid userId);
    }

}
