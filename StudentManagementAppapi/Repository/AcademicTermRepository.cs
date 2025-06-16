using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class AcademicTermRepository : IAcademicTermRepository
    {
        private readonly StudentManagementDbContext _context;

        public AcademicTermRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<AcademicTerm?> GetByIdAsync(Guid id)
        {
            return await _context.AcademicTerms.FindAsync(id);
        }

        public async Task<IEnumerable<AcademicTerm>> GetAllAsync()
        {
            return await _context.AcademicTerms.ToListAsync();
        }

        public async Task<AcademicTerm> AddAsync(AcademicTerm academicTerm)
        {
            _context.AcademicTerms.Add(academicTerm);
            await _context.SaveChangesAsync();
            return academicTerm;
        }

        public async Task<AcademicTerm> UpdateAsync(AcademicTerm academicTerm)
        {
            _context.AcademicTerms.Update(academicTerm);
            await _context.SaveChangesAsync();
            return academicTerm;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var term = await _context.AcademicTerms.FindAsync(id);
            if (term == null) return false;

            _context.AcademicTerms.Remove(term);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.AcademicTerms.AnyAsync(t => t.Id == id);
        }

        public async Task<AcademicTerm?> GetCurrentTermAsync()
        {
            return await _context.AcademicTerms
                .FirstOrDefaultAsync(t => t.IsCurrent);
        }

        public async Task<IEnumerable<AcademicTerm>> GetByAcademicYearAsync(string academicYear)
        {
            return await _context.AcademicTerms
                .Where(t => t.AcademicYear == academicYear)
                .ToListAsync();
        }

        public async Task<bool> SetCurrentTermAsync(Guid termId)
        {
          
            var allTerms = await _context.AcademicTerms.ToListAsync();
            foreach (var term in allTerms)
            {
                term.IsCurrent = false;
            }

            
            var currentTerm = await _context.AcademicTerms.FindAsync(termId);
            if (currentTerm == null) return false;

            currentTerm.IsCurrent = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
