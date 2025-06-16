
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassRepository _classRepository;

        public AnnouncementService(
            IAnnouncementRepository announcementRepository,
            IUserRepository userRepository,
            IClassRepository classRepository)
        {
            _announcementRepository = announcementRepository;
            _userRepository = userRepository;
            _classRepository = classRepository;
        }

        public async Task<ResponseWrapper<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementDto createAnnouncementDto)
        {
            try
            {
                var author = await _userRepository.GetByIdAsync(createAnnouncementDto.AuthorId);
                if (author == null)
                    return ResponseWrapper<AnnouncementDto>.Failure("Author not found");

                if (createAnnouncementDto.ClassId.HasValue)
                {
                    var classEntity = await _classRepository.GetByIdAsync(createAnnouncementDto.ClassId.Value);
                    if (classEntity == null)
                        return ResponseWrapper<AnnouncementDto>.Failure("Class not found");
                }

                var announcement = new Announcement
                {
                    Id = Guid.NewGuid(),
                    Title = createAnnouncementDto.Title,
                    Content = createAnnouncementDto.Content,
                    AuthorId = createAnnouncementDto.AuthorId,
                    ClassId = createAnnouncementDto.ClassId,
                    TargetAudience = createAnnouncementDto.TargetAudience,
                    ExpireDate = createAnnouncementDto.ExpireDate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _announcementRepository.AddAsync(announcement);

                var announcementDto = MapToDto(announcement);
                return ResponseWrapper<AnnouncementDto>.Success(announcementDto, "Announcement created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AnnouncementDto>.Failure($"Error creating announcement: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AnnouncementDto>> GetAnnouncementByIdAsync(Guid id)
        {
            try
            {
                var announcement = await _announcementRepository.GetByIdAsync(id);
                if (announcement == null)
                    return ResponseWrapper<AnnouncementDto>.Failure("Announcement not found");

                var announcementDto = MapToDto(announcement);
                return ResponseWrapper<AnnouncementDto>.Success(announcementDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AnnouncementDto>.Failure($"Error retrieving announcement: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetAllAnnouncementsAsync()
        {
            try
            {
                var announcements = await _announcementRepository.GetAllAsync();
                var announcementDtos = announcements.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Success(announcementDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Failure($"Error retrieving announcements: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AnnouncementDto>> UpdateAnnouncementAsync(Guid id, CreateAnnouncementDto updateDto)
        {
            try
            {
                var announcement = await _announcementRepository.GetByIdAsync(id);
                if (announcement == null)
                    return ResponseWrapper<AnnouncementDto>.Failure("Announcement not found");

                announcement.Title = updateDto.Title;
                announcement.Content = updateDto.Content;
                announcement.AuthorId = updateDto.AuthorId;
                announcement.ClassId = updateDto.ClassId;
                announcement.TargetAudience = updateDto.TargetAudience;
                announcement.ExpireDate = updateDto.ExpireDate;
                announcement.UpdatedAt = DateTime.UtcNow;

                await _announcementRepository.UpdateAsync(announcement);

                var announcementDto = MapToDto(announcement);
                return ResponseWrapper<AnnouncementDto>.Success(announcementDto, "Announcement updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AnnouncementDto>.Failure($"Error updating announcement: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteAnnouncementAsync(Guid id)
        {
            try
            {
                var announcement = await _announcementRepository.GetByIdAsync(id);
                if (announcement == null)
                    return ResponseWrapper<bool>.Failure("Announcement not found");

                await _announcementRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Announcement deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting announcement: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetActiveAnnouncementsAsync()
        {
            try
            {
                var announcements = await _announcementRepository.GetActiveAnnouncementsAsync();
                var announcementDtos = announcements.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Success(announcementDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Failure($"Error retrieving active announcements: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetAnnouncementsByTargetAudienceAsync(TargetAudience audience)
        {
            try
            {
                var announcements = await _announcementRepository.GetByTargetAudienceAsync(audience);
                var announcementDtos = announcements.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Success(announcementDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Failure($"Error retrieving announcements: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetAnnouncementsByClassAsync(Guid classId)
        {
            try
            {
                var announcements = await _announcementRepository.GetByClassIdAsync(classId);
                var announcementDtos = announcements.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Success(announcementDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AnnouncementDto>>.Failure($"Error retrieving class announcements: {ex.Message}");
            }
        }

        
        private AnnouncementDto MapToDto(Announcement announcement)
        {
            return new AnnouncementDto
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Content = announcement.Content,
                AuthorId = announcement.AuthorId,
                ClassId = announcement.ClassId,
                TargetAudience = announcement.TargetAudience,
                ExpireDate = announcement.ExpireDate,
                CreatedAt = announcement.CreatedAt,
                UpdatedAt = announcement.UpdatedAt
            };
        }
    }


}
