using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IClassSubjectRepository
    {
       
        Task<ClassSubject?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassSubject>> GetAllAsync();
        Task<ClassSubject> AddAsync(ClassSubject classSubject);
        Task<ClassSubject> UpdateAsync(ClassSubject classSubject);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);

       
        Task<IEnumerable<ClassSubject>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<ClassSubject>> GetBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<ClassSubject>> GetByTeacherIdAsync(Guid teacherId);
        Task<ClassSubject?> GetByClassAndSubjectAsync(Guid classId, Guid subjectId);
    }

}
