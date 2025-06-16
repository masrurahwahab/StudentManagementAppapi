using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentRepository _studentRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IStudentRepository studentRepository)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
        }

        public async Task<ResponseWrapper<AttendanceResponse>> MarkAttendanceAsync(MarkAttendanceDto markAttendanceDto)
        {
            try
            {

                var existingAttendance = await _attendanceRepository.GetByStudentAndDateAsync(
                    markAttendanceDto.StudentId, markAttendanceDto.Date);

                if (existingAttendance != null)
                {
                   
                    existingAttendance.Status = markAttendanceDto.Status;
                    existingAttendance.Remarks = markAttendanceDto.Remarks;
                    existingAttendance.UpdatedAt = DateTime.Now;

                    var updatedAttendance = await _attendanceRepository.UpdateAsync(existingAttendance);
                    var updatedDto = await MapToAttendanceDto(updatedAttendance);

                    return new ResponseWrapper<AttendanceResponse>
                    {
                        Successs = true,
                        Message = "Attendance updated successfully",
                        Data = updatedDto
                    };
                }
                else
                {
                   
                    var attendance = new Attendance
                    {
                        StudentId = markAttendanceDto.StudentId,
                        Date = markAttendanceDto.Date,
                        Status = markAttendanceDto.Status,
                        Remarks = markAttendanceDto.Remarks
                    };

                    var createdAttendance = await _attendanceRepository.AddAsync(attendance);
                    var attendanceDto = await MapToAttendanceDto(createdAttendance);

                    return new ResponseWrapper<AttendanceResponse>
                    {
                        Successs = true,
                        Message = "Attendance marked successfully",
                        Data = attendanceDto
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<AttendanceResponse>
                {
                    Successs = false,
                    Message = $"Error marking attendance: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByStudentIdAsync(Guid studentId)
        {
            try
            {
                var attendanceRecords = await _attendanceRepository.GetByStudentIdAsync(studentId);
                var attendanceDtos = new List<AttendanceResponse>();

                foreach (var attendance in attendanceRecords)
                {
                    attendanceDtos.Add(await MapToAttendanceDto(attendance));
                }

                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = true,
                    Data = attendanceDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving attendance: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByDateAsync(DateTime date)
        {
            try
            {
                var attendanceRecords = await _attendanceRepository.GetByDateAsync(date);
                var attendanceDtos = new List<AttendanceResponse>();

                foreach (var attendance in attendanceRecords)
                {
                    attendanceDtos.Add(await MapToAttendanceDto(attendance));
                }

                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = true,
                    Data = attendanceDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving attendance: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AttendanceResponse>>> GetAttendanceByClassAndDateAsync(Guid classId, DateTime date)
        {
            try
            {
                var attendanceRecords = await _attendanceRepository.GetByClassAndDateAsync(classId, date);
                var attendanceDtos = new List<AttendanceResponse>();

                foreach (var attendance in attendanceRecords)
                {
                    attendanceDtos.Add(await MapToAttendanceDto(attendance));
                }

                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = true,
                    Data = attendanceDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<AttendanceResponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving attendance: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<AttendanceResponse>> UpdateAttendanceAsync(Guid id, MarkAttendanceDto updateDto)
        {
            try
            {
                var attendance = await _attendanceRepository.GetByIdAsync(id);
                if (attendance == null)
                {
                    return new ResponseWrapper<AttendanceResponse>
                    {
                        Successs = false,
                        Message = "Attendance record not found"
                    };
                }

                attendance.Status = updateDto.Status;
                attendance.Remarks = updateDto.Remarks;
                attendance.UpdatedAt = DateTime.Now;

                var updatedAttendance = await _attendanceRepository.UpdateAsync(attendance);
                var attendanceDto = await MapToAttendanceDto(updatedAttendance);

                return new ResponseWrapper<AttendanceResponse>
                {
                    Successs = true,
                    Message = "Attendance updated successfully",
                    Data = attendanceDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<AttendanceResponse>
                {
                    Successs = false,
                    Message = $"Error updating attendance: {ex.Message}"
                };
            }
        }

        private async Task<AttendanceResponse> MapToAttendanceDto(Attendance attendance)
        {
            var student = await _studentRepository.GetWithUserAsync(attendance.StudentId);

            return new AttendanceResponse
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                StudentName = student != null ? $"{student.User.FirstName} {student.User.LastName}" : "",
                Date = attendance.Date,
                Status = attendance.Status,
                Remarks = attendance.Remarks
            };
        }
    }

}
