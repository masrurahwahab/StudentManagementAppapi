using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly StudentManagementDbContext _context;
        private readonly DbSet<Class> _dbSet;

        public ClassRepository(StudentManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Class>();
        }

        public async Task<Class> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Class> AddAsync(Class entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Class> UpdateAsync(Class entity)
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

        public async Task<IEnumerable<Class>> GetByAcademicYearAsync(string academicYear)
        {
            return await _dbSet
                .Include(c => c.ClassTeacher)
                .ThenInclude(t => t.User)
                .Where(c => c.AcademicYear == academicYear)
                .ToListAsync();
        }

        public async Task<Class> GetWithStudentsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Students)
                .ThenInclude(s => s.User)
                .Include(c => c.ClassTeacher)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Class> GetWithSubjectsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.ClassSubjects)
                .ThenInclude(cs => cs.Subject)
                .Include(c => c.ClassSubjects)
                .ThenInclude(cs => cs.Teacher)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CanAddStudentAsync(Guid classId)
        {
            var classEntity = await GetByIdAsync(classId);
            return classEntity != null && classEntity.CurrentStudentsCount < classEntity.MaxStudents;
        }
    }
}
