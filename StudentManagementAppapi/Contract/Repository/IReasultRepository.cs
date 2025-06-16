using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IResultRepository
    {
        Task<Result> GetByIdAsync(Guid id);
        Task<IEnumerable<Result>> GetAllAsync();
        Task<Result> AddAsync(Result entity);
        Task<Result> UpdateAsync(Result entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Result>> GetByStudentIdAsync(string admissionNo);
        Task<IEnumerable<Result>> GetByAssessmentIdAsync(Guid assessmentId);
        Task<IEnumerable<Result>> GetByStudentAndTermAsync(string admissionNo, Guid termId);
        Task<Result> GetByStudentAndAssessmentAsync(string admissionNo, Guid assessmentId);
    }

}
