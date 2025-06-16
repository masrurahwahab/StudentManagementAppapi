using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentManagementDbContext _context;

        public SubjectRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> AddAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Subjects.AnyAsync(s => s.Id == id);
        }

        public async Task<Subject?> GetByCodeAsync(string code)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<IEnumerable<Subject>> GetByClassLevelAsync(ClassLevel classLevel)
        {
            return await _context.Subjects
                .Where(s => s.ClassLevel == classLevel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetCoreSubjectsAsync()
        {
            return await _context.Subjects
                .Where(s => s.IsCore)
                .ToListAsync();
        }
    }


}
