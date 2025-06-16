using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IDisciplinaryRepository
    {
        
        Task<Disciplinary?> GetByIdAsync(Guid id);
        Task<IEnumerable<Disciplinary>> GetAllAsync();
        Task<Disciplinary> AddAsync(Disciplinary disciplinary);
        Task<Disciplinary> UpdateAsync(Disciplinary disciplinary);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

        
        Task<IEnumerable<Disciplinary>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<Disciplinary>> GetByStatusAsync(DisciplinaryStatus status);
        Task<IEnumerable<Disciplinary>> GetByReporterIdAsync(Guid reporterId);
        Task<IEnumerable<Disciplinary>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }

}
