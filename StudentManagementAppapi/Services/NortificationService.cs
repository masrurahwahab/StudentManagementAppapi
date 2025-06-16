using AutoMapper;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseWrapper<NotificationDto>> CreateNotificationAsync(CreateNotificationDto createNotificationDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(createNotificationDto.UserId);
                if (user == null)
                {
                    return ResponseWrapper<NotificationDto>.Failure("User not found");
                }

                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = createNotificationDto.UserId,
                    Title = createNotificationDto.Title,
                    Message = createNotificationDto.Message,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };

                await _notificationRepository.AddAsync(notification);

                var notificationDto = MapToDto(notification);
                return ResponseWrapper<NotificationDto>.Success(notificationDto, "Notification created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<NotificationDto>.Failure($"Error creating notification: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<NotificationDto>> GetNotificationByIdAsync(Guid id)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(id);
                if (notification == null)
                {
                    return ResponseWrapper<NotificationDto>.Failure("Notification not found");
                }

                var notificationDto = MapToDto(notification);
                return ResponseWrapper<NotificationDto>.Success(notificationDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<NotificationDto>.Failure($"Error retrieving notification: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetAllNotificationsAsync()
        {
            try
            {
                var notifications = await _notificationRepository.GetAllAsync();
                var notificationDtos = notifications.Select(MapToDto);
                return ResponseWrapper<IEnumerable<NotificationDto>>.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<NotificationDto>>.Failure($"Error retrieving notifications: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetNotificationsByUserIdAsync(Guid userId)
        {
            try
            {
                var notifications = await _notificationRepository.GetByUserIdAsync(userId);
                var notificationDtos = notifications.Select(MapToDto);
                return ResponseWrapper<IEnumerable<NotificationDto>>.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<NotificationDto>>.Failure($"Error retrieving notifications: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<NotificationDto>>> GetUnreadNotificationsByUserIdAsync(Guid userId)
        {
            try
            {
                var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);
                var notificationDtos = notifications.Select(MapToDto);
                return ResponseWrapper<IEnumerable<NotificationDto>>.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<NotificationDto>>.Failure($"Error retrieving unread notifications: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> MarkNotificationAsReadAsync(Guid notificationId)
        {
            try
            {
                var result = await _notificationRepository.MarkAsReadAsync(notificationId);
                if (!result)
                {
                    return ResponseWrapper<bool>.Failure("Notification not found");
                }

                return ResponseWrapper<bool>.Success(true, "Notification marked as read");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error marking notification as read: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> MarkAllNotificationsAsReadAsync(Guid userId)
        {
            try
            {
                var result = await _notificationRepository.MarkAllAsReadAsync(userId);
                return ResponseWrapper<bool>.Success(result, "All notifications marked as read");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error marking all notifications as read: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteNotificationAsync(Guid id)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(id);
                if (notification == null)
                {
                    return ResponseWrapper<bool>.Failure("Notification not found");
                }

                await _notificationRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Notification deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting notification: {ex.Message}");
            }
        }

        private NotificationDto MapToDto(Notification notification)
        {
            return new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }
    }


}
