using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IParentRepository
    {
        
        Task<Parent?> GetByIdAsync(Guid id);
        Task<IEnumerable<Parent>> GetAllAsync();
        Task<Parent> AddAsync(Parent parent);
        Task<Parent> UpdateAsync(Parent parent);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

       
        Task<Parent?> GetByUserIdAsync(Guid userId);
        Task<Parent?> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<Parent>> GetByStudentIdsAsync(IEnumerable<Guid> studentIds);
    }

}
