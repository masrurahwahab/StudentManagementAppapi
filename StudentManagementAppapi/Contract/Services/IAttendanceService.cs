using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Contract.Services
{
    public interface IAttendanceService
    {
        Task<ResponseWrapper<AttendanceResponse>> MarkAttendanceAsync(MarkAttendanceDto markAttendanceDto);
        Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByStudentIdAsync(Guid studentId);
        Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByDateAsync(DateTime date);
        Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByClassAndDateAsync(Guid classId, DateTime date);
        Task<ResponseWrapper<AttendanceResponse>> UpdateAttendanceAsync(Guid id, MarkAttendanceDto updateDto);
    }
}
