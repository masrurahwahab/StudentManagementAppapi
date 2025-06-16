using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ITermReportRepository
    {
       
        Task<TermReport?> GetByIdAsync(Guid id);
        Task<IEnumerable<TermReport>> GetAllAsync();
        Task<TermReport> AddAsync(TermReport termReport);
        Task<TermReport> UpdateAsync(TermReport termReport);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

        
        Task<TermReport?> GetByStudentAndTermAsync(string adissionNo, Guid termId);
        Task<IEnumerable<TermReport>> GetByTermAsync(Guid termId);
        Task<IEnumerable<TermReport>> GetByStudentAsync(string admissionNo);
        Task<IEnumerable<TermReport>> GetByClassAndTermAsync(Guid classId, Guid termId);
    }

}
