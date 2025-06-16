using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IClassRepository
    {
        Task<Class> GetByIdAsync(Guid id);
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class> AddAsync(Class entity);
        Task<Class> UpdateAsync(Class entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Class>> GetByAcademicYearAsync(string academicYear);
        Task<Class> GetWithStudentsAsync(Guid id);
        Task<Class> GetWithSubjectsAsync(Guid id);
        Task<bool> CanAddStudentAsync(Guid classId);
    }
}
