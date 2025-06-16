using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Repository
{
   

    public class DisciplinaryRepository : IDisciplinaryRepository
    {
        private readonly StudentManagementDbContext _context;

        public DisciplinaryRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Disciplinary?> GetByIdAsync(Guid id)
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Include(d => d.ReportedBy)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Disciplinary>> GetAllAsync()
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Include(d => d.ReportedBy)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<Disciplinary> AddAsync(Disciplinary disciplinary)
        {
            
            if (disciplinary.DateReported == default)
            {
                disciplinary.DateReported = DateTime.UtcNow;
            }

            _context.Disciplinaries.Add(disciplinary);
            await _context.SaveChangesAsync();
            return disciplinary;
        }

        public async Task<Disciplinary> UpdateAsync(Disciplinary disciplinary)
        {
            _context.Disciplinaries.Update(disciplinary);
            await _context.SaveChangesAsync();
            return disciplinary;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var disciplinary = await _context.Disciplinaries.FindAsync(id);
            if (disciplinary == null) return false;

            _context.Disciplinaries.Remove(disciplinary);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Disciplinaries.AnyAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Disciplinary>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.Disciplinaries
                .Include(d => d.ReportedBy)
                .Where(d => d.StudentId == studentId)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplinary>> GetByStatusAsync(DisciplinaryStatus status)
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Include(d => d.ReportedBy)
                .Where(d => d.Status == status)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplinary>> GetByReporterIdAsync(Guid reporterId)
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Where(d => d.ReportedById == reporterId)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplinary>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Include(d => d.ReportedBy)
                .Where(d => d.DateReported >= startDate && d.DateReported <= endDate)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

      
        public async Task<IEnumerable<Disciplinary>> GetPendingDisciplinaryActionsAsync()
        {
            return await _context.Disciplinaries
                .Include(d => d.Student)
                .Include(d => d.ReportedBy)
                .Where(d => d.Status == DisciplinaryStatus.Pending)
                .OrderBy(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<IEnumerable<Disciplinary>> GetByStudentAndStatusAsync(Guid studentId, DisciplinaryStatus status)
        {
            return await _context.Disciplinaries
                .Include(d => d.ReportedBy)
                .Where(d => d.StudentId == studentId && d.Status == status)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();
        }

        public async Task<int> GetDisciplinaryCountByStudentAsync(Guid studentId)
        {
            return await _context.Disciplinaries
                .CountAsync(d => d.StudentId == studentId);
        }

        public async Task<int> GetDisciplinaryCountByStatusAsync(DisciplinaryStatus status)
        {
            return await _context.Disciplinaries
                .CountAsync(d => d.Status == status);
        }
    }

}
