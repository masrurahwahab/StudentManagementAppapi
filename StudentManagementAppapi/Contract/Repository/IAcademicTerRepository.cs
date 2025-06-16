using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IAcademicTermRepository
    {
      
        Task<AcademicTerm?> GetByIdAsync(Guid id);
        Task<IEnumerable<AcademicTerm>> GetAllAsync();
        Task<AcademicTerm> AddAsync(AcademicTerm academicTerm);
        Task<AcademicTerm> UpdateAsync(AcademicTerm academicTerm);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

        
        Task<AcademicTerm?> GetCurrentTermAsync();
        Task<IEnumerable<AcademicTerm>> GetByAcademicYearAsync(string academicYear);
        Task<bool> SetCurrentTermAsync(Guid termId);
    }

}
