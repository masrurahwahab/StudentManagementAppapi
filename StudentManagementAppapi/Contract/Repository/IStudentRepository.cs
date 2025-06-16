using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using System.Linq.Expressions;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IStudentRepository
    {
        Task<int> GetCountAsync();
        Task<Student> GetByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> AddAsync(Student entity);
        Task<Student> UpdateAsync(Student entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<Student> GetByAdmissionNumberAsync(string admissionNumber);
        Task<IEnumerable<Student>> GetByClassIdAsync(Guid classId);
        Task<PagedResponse<Student>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null);
        Task<Student> GetWithUserAsync(Guid id);
    }

}
