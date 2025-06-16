using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
   
    public class ParentRepository : IParentRepository
    {
        private readonly StudentManagementDbContext _context;

        public ParentRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Parent?> GetByIdAsync(Guid id)
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Parent>> GetAllAsync()
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Student)
                .ToListAsync();
        }

        public async Task<Parent> AddAsync(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
            return parent;
        }

        public async Task<Parent> UpdateAsync(Parent parent)
        {
            _context.Parents.Update(parent);
            await _context.SaveChangesAsync();
            return parent;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null) return false;

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Parents.AnyAsync(p => p.Id == id);
        }

        public async Task<Parent?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Parent?> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.StudentId == studentId);
        }

        public async Task<IEnumerable<Parent>> GetByStudentIdsAsync(IEnumerable<Guid> studentIds)
        {
            return await _context.Parents
                .Include(p => p.User)
                .Include(p => p.Student)
                .Where(p => studentIds.Contains(p.StudentId))
                .ToListAsync();
        }
    }
   
}
