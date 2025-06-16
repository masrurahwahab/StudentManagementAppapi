using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IAssessmentRepository
    {
       
        Task<Assessment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Assessment>> GetAllAsync();
        Task<Assessment> AddAsync(Assessment assessment);
        Task<Assessment> UpdateAsync(Assessment assessment);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

       
        Task<IEnumerable<Assessment>> GetByClassAsync(Guid classId);
        Task<IEnumerable<Assessment>> GetByTermAsync(Guid termId);
        Task<IEnumerable<Assessment>> GetByClassAndTermAsync(Guid classId, Guid termId);
        Task<IEnumerable<Assessment>> GetByTeacherAsync(Guid teacherId);
    }

}
