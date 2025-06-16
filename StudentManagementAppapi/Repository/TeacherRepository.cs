using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace StudentManagementAppapi.Repository
{
  
    public class TeacherRepository : ITeacherRepository
    {
        private readonly StudentManagementDbContext _context;

        public TeacherRepository(StudentManagementDbContext context)
        {
            _context = context;
        }

        
        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Class)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .OrderBy(t => t.User.LastName)
                .ThenBy(t => t.User.FirstName)
                .ToListAsync();
        }

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
           
            if (teacher.HireDate == default)
            {
                teacher.HireDate = DateTime.UtcNow;
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher> UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

       

        public async Task<bool> DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id) 
        {
            return await _context.Teachers.AnyAsync(t => t.Id == id);
        }

        public async Task<Teacher?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<Teacher?> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .FirstOrDefaultAsync(t => t.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Teacher>> GetBySubjectAsync(string subject)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Where(t => t.ClassSubjects.Any(cs => cs.Subject.Name == subject))
                .OrderBy(t => t.User.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByDepartmentAsync(string department)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Where(t => t.Department == department)
                .OrderBy(t => t.User.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetClassTeachersAsync()
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Where(t => t.IsClassTeacher)
                .OrderBy(t => t.Class!.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByStatusAsync(string status)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Where(t => t.Status == status)
                .OrderBy(t => t.User.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetActiveTeachersAsync()
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Where(t => t.Status == "Active" && t.TerminationDate == null)
                .OrderBy(t => t.User.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Where(t => t.HireDate >= startDate && t.HireDate <= endDate)
                .OrderBy(t => t.HireDate)
                .ToListAsync();
        }

        public async Task<Teacher?> GetClassTeacherByClassIdAsync(Guid classId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Class)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .FirstOrDefaultAsync(t => t.ClassId == classId && t.IsClassTeacher);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersByClassAsync(Guid classId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .Where(t => t.ClassSubjects.Any(cs => cs.ClassId == classId))
                .Distinct()
                .OrderBy(t => t.User.LastName)
                .ToListAsync();
        }

        public async Task<int> GetTeacherCountByDepartmentAsync(string department)
        {
            return await _context.Teachers
                .CountAsync(t => t.Department == department && t.Status == "Active");
        }

        public async Task<int> GetActiveTeacherCountAsync()
        {
            return await _context.Teachers
                .CountAsync(t => t.Status == "Active" && t.TerminationDate == null);
        }

        public async Task<IEnumerable<string>> GetAllDepartmentsAsync()
        {
            return await _context.Teachers
                .Where(t => !string.IsNullOrEmpty(t.Department))
                .Select(t => t.Department!)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();
        }

        public async Task<bool> AssignClassTeacherAsync(Guid teacherId, Guid classId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher == null) return false;

           
            var existingClassTeacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.ClassId == classId && t.IsClassTeacher);
            if (existingClassTeacher != null)
            {
                existingClassTeacher.IsClassTeacher = false;
                existingClassTeacher.ClassId = null;
            }

          
            teacher.IsClassTeacher = true;
            teacher.ClassId = classId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveClassTeacherAsync(Guid teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher == null) return false;

            teacher.IsClassTeacher = false;
            teacher.ClassId = null;

            await _context.SaveChangesAsync();
            return true;
        }
      
    }
}

