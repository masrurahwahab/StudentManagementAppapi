using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IAnnouncementRepository
    {
       
        Task<Announcement?> GetByIdAsync(Guid id);
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<Announcement> AddAsync(Announcement announcement);
        Task<Announcement> UpdateAsync(Announcement announcement);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

        
        Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync();
        Task<IEnumerable<Announcement>> GetByTargetAudienceAsync(TargetAudience audience);
        Task<IEnumerable<Announcement>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<Announcement>> GetByAuthorIdAsync(Guid authorId);
    }

}
