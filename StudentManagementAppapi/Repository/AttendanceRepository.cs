using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly StudentManagementDbContext _context;
        private readonly DbSet<Attendance> _dbSet;

        public AttendanceRepository(StudentManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Attendance>();
        }

        public async Task<Attendance> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Attendance> AddAsync(Attendance entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Attendance> UpdateAsync(Attendance entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Attendance>> GetByStudentIdAsync(Guid studentId)
        {
            return await _dbSet
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetByDateAsync(DateTime date)
        {
            return await _dbSet
                .Include(a => a.Student)
                .ThenInclude(s => s.User)
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetByClassAndDateAsync(Guid classId, DateTime date)
        {
            return await _dbSet
                .Include(a => a.Student)
                .ThenInclude(s => s.User)
                .Where(a => a.Student.ClassId == classId && a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Attendance> GetByStudentAndDateAsync(Guid studentId, DateTime date)
        {
            return await _dbSet
                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.Date.Date == date.Date);
        }
    }


}
