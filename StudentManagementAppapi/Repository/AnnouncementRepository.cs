using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly StudentManagementDbContext _context;

        public AnnouncementRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement?> GetByIdAsync(Guid id)
        {
            return await _context.Announcements
                .Include(a => a.Author)
                .Include(a => a.Class)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _context.Announcements
                .Include(a => a.Author)
                .Include(a => a.Class)
                .ToListAsync();
        }

        public async Task<Announcement> AddAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<Announcement> UpdateAsync(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return false;

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Announcements.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Announcement>> GetActiveAnnouncementsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Announcements
                .Include(a => a.Author)
                .Include(a => a.Class)
                .Where(a => a.StartDate <= now && a.ExpireDate >= now)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetByTargetAudienceAsync(TargetAudience audience)
        {
            return await _context.Announcements
                .Include(a => a.Author)
                .Include(a => a.Class)
                .Where(a => a.TargetAudience == audience)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetByClassIdAsync(Guid classId)
        {
            return await _context.Announcements
                .Include(a => a.Author)
                .Where(a => a.ClassId == classId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Announcement>> GetByAuthorIdAsync(Guid authorId)
        {
            return await _context.Announcements
                .Include(a => a.Class)
                .Where(a => a.AuthorId == authorId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }


}
