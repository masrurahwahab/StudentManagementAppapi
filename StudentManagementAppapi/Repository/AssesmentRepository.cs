using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementAppapi.Repository
{   

    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly StudentManagementDbContext _context;

        public AssessmentRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Assessment?> GetByIdAsync(Guid id)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Class)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .Include(a => a.Results)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Assessment>> GetAllAsync()
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Class)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .ToListAsync();
        }

        public async Task<Assessment> AddAsync(Assessment assessment)
        {
            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<Assessment> UpdateAsync(Assessment assessment)
        {
            _context.Assessments.Update(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null) return false;

            _context.Assessments.Remove(assessment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Assessments.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Assessment>> GetByClassAsync(Guid classId)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .Where(a => a.ClassSubject.ClassId == classId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetByTermAsync(Guid termId)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Class)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .Where(a => a.TermId == termId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetByClassAndTermAsync(Guid classId, Guid termId)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .Where(a => a.ClassSubject.ClassId == classId && a.TermId == termId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetByTeacherAsync(Guid teacherId)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Class)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(a => a.Term)
                .Where(a => a.ClassSubject.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Assessment>> GetBySubjectAsync(Guid subjectId)
        {
            return await _context.Assessments
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Class)
                .Include(a => a.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(a => a.Term)
                .Where(a => a.ClassSubject.SubjectId == subjectId)
                .ToListAsync();
        }
    }
}
