using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetByIdAsync(Guid id);
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task<Attendance> AddAsync(Attendance entity);
        Task<Attendance> UpdateAsync(Attendance entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Attendance>> GetByStudentIdAsync(Guid studentId);
        Task<IEnumerable<Attendance>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Attendance>> GetByClassAndDateAsync(Guid classId, DateTime date);
        Task<Attendance> GetByStudentAndDateAsync(Guid studentId, DateTime date);
    }


}
