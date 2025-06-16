using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ITeacherRepository
    {       
        Task<Teacher?> GetByIdAsync(Guid id);
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> AddAsync(Teacher teacher);
        Task<Teacher> UpdateAsync(Teacher teacher);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

        
        Task<Teacher?> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Teacher>> GetBySubjectAsync(string subject);
        Task<IEnumerable<Teacher>> GetByDepartmentAsync(string department);
    }
}
