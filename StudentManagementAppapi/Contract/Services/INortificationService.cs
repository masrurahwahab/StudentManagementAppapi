using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface INotificationService
    {
        Task<ResponseWrapper<NotificationDto>> CreateNotificationAsync(CreateNotificationDto createNotificationDto);
        Task<ResponseWrapper<NotificationDto>> GetNotificationByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetAllNotificationsAsync();
        Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetNotificationsByUserIdAsync(Guid userId);
        Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetUnreadNotificationsByUserIdAsync(Guid userId);
        Task<ResponseWrapper<bool>> MarkNotificationAsReadAsync(Guid notificationId);
        Task<ResponseWrapper<bool>> MarkAllNotificationsAsReadAsync(Guid userId);
        Task<ResponseWrapper<bool>> DeleteNotificationAsync(Guid id);
    }
}
