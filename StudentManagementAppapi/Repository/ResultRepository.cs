using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly StudentManagementDbContext _context;
        private readonly DbSet<Result> _dbSet;

        public ResultRepository(StudentManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Result>();
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Result> AddAsync(Result entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Result> UpdateAsync(Result entity)
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

        public async Task<IEnumerable<Result>> GetByStudentIdAsync(string admissioNo)
        {
            return await _dbSet
                .Include(r => r.Assessment)
                .ThenInclude(a => a.ClassSubject)
                .ThenInclude(cs => cs.Subject)
                .Where(r => r.Student.AdmissionNumber == admissioNo)
                .OrderByDescending(r => r.RecordedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetByAssessmentIdAsync(Guid assessmentId)
        {
            return await _dbSet
                .Include(r => r.Student)
                .ThenInclude(s => s.User)
                .Where(r => r.AssessmentId == assessmentId)
                .OrderBy(r => r.Student.User.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetByStudentAndTermAsync(string admissionNo, Guid termId)
        {
            return await _dbSet
                .Include(r => r.Assessment)
                .ThenInclude(a => a.ClassSubject)
                .ThenInclude(cs => cs.Subject)
                .Where(r => r.Student.AdmissionNumber == admissionNo && r.Assessment.TermId == termId)
                .ToListAsync();
        }

        public async Task<Result> GetByStudentAndAssessmentAsync(string admissionNo, Guid assessmentId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.Student.AdmissionNumber == admissionNo && r.AssessmentId == assessmentId);
        }
    }


}
