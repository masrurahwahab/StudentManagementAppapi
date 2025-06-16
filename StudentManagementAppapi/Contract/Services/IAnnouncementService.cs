using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IAnnouncementService
    {
        Task<ResponseWrapper<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementDto createAnnouncementDto);
        Task<ResponseWrapper<AnnouncementDto>> GetAnnouncementByIdAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetAllAnnouncementsAsync();
        Task<ResponseWrapper<AnnouncementDto>> UpdateAnnouncementAsync(Guid id, CreateAnnouncementDto updateDto);
        Task<ResponseWrapper<bool>> DeleteAnnouncementAsync(Guid id);
        Task<ResponseWrapper<IEnumerable<AnnouncementDto>>> GetActiveAnnouncementsAsync();
    }

}
