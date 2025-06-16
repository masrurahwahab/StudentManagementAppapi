using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class ClassSubjectRepository : IClassSubjectRepository
    {
        private readonly StudentManagementDbContext _context;

        public ClassSubjectRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<ClassSubject?> GetByIdAsync(Guid id)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task<IEnumerable<ClassSubject>> GetAllAsync()
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .ToListAsync();
        }

        public async Task<ClassSubject> AddAsync(ClassSubject classSubject)
        {
            _context.ClassSubjects.Add(classSubject);
            await _context.SaveChangesAsync();
            return classSubject;
        }

        public async Task<ClassSubject> UpdateAsync(ClassSubject classSubject)
        {
            _context.ClassSubjects.Update(classSubject);
            await _context.SaveChangesAsync();
            return classSubject;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var classSubject = await _context.ClassSubjects.FindAsync(id);
            if (classSubject == null) return false;

            _context.ClassSubjects.Remove(classSubject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.ClassSubjects.AnyAsync(cs => cs.Id == id);
        }

        public async Task<IEnumerable<ClassSubject>> GetByClassIdAsync(Guid classId)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .Where(cs => cs.ClassId == classId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassSubject>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Teacher)
                .Where(cs => cs.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassSubject>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Where(cs => cs.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<ClassSubject?> GetByClassAndSubjectAsync(Guid classId, Guid subjectId)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .FirstOrDefaultAsync(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        }
    }


}
