using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ISubjectRepository
    {
      
        Task<Subject?> GetByIdAsync(Guid id);
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> AddAsync(Subject subject);
        Task<Subject> UpdateAsync(Subject subject);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

       
        Task<Subject?> GetByCodeAsync(string code);
        Task<IEnumerable<Subject>> GetByClassLevelAsync(ClassLevel classLevel);
        Task<IEnumerable<Subject>> GetCoreSubjectsAsync();
    }


}
