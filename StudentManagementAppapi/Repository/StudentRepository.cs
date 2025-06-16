using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using System.Linq.Expressions;

namespace StudentManagementAppapi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentManagementDbContext _context;
        private readonly DbSet<Student> _dbSet;

        public StudentRepository(StudentManagementDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Student>();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Students.CountAsync();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Student> AddAsync(Student entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> UpdateAsync(Student entity)
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

        public async Task<Student> GetByAdmissionNumberAsync(string admissionNumber)
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.AdmissionNumber == admissionNumber);
        }

        public async Task<IEnumerable<Student>> GetByClassIdAsync(Guid classId)
        {
            return await _dbSet
                .Include(s => s.User)
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }

        public async Task<PagedResponse<Student>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _dbSet
                .Include(s => s.User)
                .Include(s => s.Class)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => s.User.FirstName.Contains(searchTerm) ||
                                   s.User.LastName.Contains(searchTerm) ||
                                   s.AdmissionNumber.Contains(searchTerm));
            }

            var totalRecords = await query.CountAsync();
            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Student>
            {
                Data = students,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };
        }

        public async Task<Student> GetWithUserAsync(Guid id)
        {
            return await _dbSet
                .Include(s => s.User)
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }

}
