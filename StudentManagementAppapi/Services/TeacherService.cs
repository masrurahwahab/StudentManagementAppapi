using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.PasswordValidation;

namespace StudentManagementAppapi.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPasswordHashing _userService;
        private readonly StudentManagementDbContext _student;


        public TeacherService(ITeacherRepository teacherRepository, IPasswordHashing userService,
            StudentManagementDbContext studentManagementDbContext)
        {
            _teacherRepository = teacherRepository;
            _userService = userService;
            _student = studentManagementDbContext;
        }



        public async Task<ResponseWrapper<TeacherDto>> CreateTeacherAsync(CreateTeacherDto dto)
        {
            try
            {
                var salt = _userService.GenerateSalt();
                var passwordHash = _userService.HashPassword(dto.Password, salt);

                var teacher = new Teacher
                {
                    Phone = dto.Phone,
                    EmployeeId = dto.EmployeeId,
                    IsClassTeacher = dto.IsClassTeacher,
                    HireDate = dto.HireDate,
                    Qualification = dto.Qualification,
                    CreatedAt = DateTime.UtcNow,
                    Status = "Active",
                    User = new User
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        PasswordHash = passwordHash,
                        Salt = salt
                    }
                   
                };

                
                foreach (var subjectName in dto.SubjectsTaught)
                {
                    var trimmedName = subjectName.Trim();

                   
                    var existingSubject = await _student.Subjects 
                        .FirstOrDefaultAsync(s => s.Name == trimmedName);

                    if (existingSubject == null)
                    {
                        var newSubject = new Subject
                        {
                            Name = trimmedName,
                            Code = GenerateUniqueSubjectCode(trimmedName)
                        };
                        _student.Subjects.Add(newSubject);  
                    }

                   
                }

                await _teacherRepository.AddAsync(teacher);
              

                var teacherDto = MapToDto(teacher);
                return ResponseWrapper<TeacherDto>.Success(teacherDto, "Teacher created successfully");
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return ResponseWrapper<TeacherDto>.Failure($"Error creating teacher: {error}");
            }
        }

        private string GenerateUniqueSubjectCode(string subjectName)
        {
            var baseCode = new string(subjectName.Where(char.IsLetter).Take(3).ToArray()).ToUpper();
            var uniqueSuffix = DateTime.UtcNow.Ticks % 10000;
            return $"{baseCode}{uniqueSuffix}";
        }


        public async Task<ResponseWrapper<TeacherDto>> GetTeacherByIdAsync(Guid id)
        {
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                return teacher == null
                    ? ResponseWrapper<TeacherDto>.Failure("Teacher not found")
                    : ResponseWrapper<TeacherDto>.Success(MapToDto(teacher));
            }
            catch (Exception ex)
            {
                return ResponseWrapper<TeacherDto>.Failure($"Error retrieving teacher: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TeacherDto>>> GetAllTeachersAsync()
        {
            try
            {
                var teachers = await _teacherRepository.GetAllAsync();
                var teacherDtos = teachers.Select(MapToDto);
                return ResponseWrapper<IEnumerable<TeacherDto>>.Success(teacherDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TeacherDto>>.Failure($"Error retrieving teachers: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<TeacherDto>> UpdateTeacherAsync(Guid id, UpdateTeacherDto dto)
        {
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher == null)
                    return ResponseWrapper<TeacherDto>.Failure("Teacher not found");

               
                teacher.EmployeeId = dto.EmployeeId;
                teacher.IsClassTeacher = dto.IsClassTeacher;
                teacher.ClassId = dto.ClassId;
                teacher.HireDate = dto.HireDate;
                teacher.Qualification = dto.Qualification;

                
                teacher.ClassSubjects.Clear();

                
                teacher.ClassSubjects = dto.SubjectsTaught
                    .Select(subjectName => new ClassSubject
                    {
                        Subject = new Subject { Name = subjectName.Trim() },
                        Teacher = teacher
                    }).ToList();

                
                await _teacherRepository.UpdateAsync(teacher);

                return ResponseWrapper<TeacherDto>.Success(MapToDto(teacher), "Teacher updated successfully");
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                return ResponseWrapper<TeacherDto>.Failure($"Error updating teacher: {error}");
            }
        }


        public async Task<ResponseWrapper<bool>> DeleteTeacherAsync(Guid id)
        {
            try
            {
                var result = await _teacherRepository.DeleteAsync(id);
                return result
                    ? ResponseWrapper<bool>.Success(true, "Teacher deleted successfully")
                    : ResponseWrapper<bool>.Failure("Teacher not found");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting teacher: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<TeacherDto>>> GetTeachersBySubjectAsync(string subject)
        {
            try
            {
                var teachers = await _teacherRepository.GetBySubjectAsync(subject);
                var teacherDtos = teachers.Select(MapToDto);
                return ResponseWrapper<IEnumerable<TeacherDto>>.Success(teacherDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<TeacherDto>>.Failure($"Error retrieving teachers by subject: {ex.Message}");
            }
        }

       
        private TeacherDto MapToDto(Teacher teacher)
        {
            return new TeacherDto
            {
                User = new UserResponseModel
                {
                    FirstName =teacher.User?.FirstName,
                    LastName = teacher.User?.LastName,
                    Email = teacher.User?.Email,
                },
                Id = teacher.Id,
                EmployeeId = teacher.EmployeeId,
                Phone = teacher.Phone,
                Qualification = teacher.Qualification,
                HireDate = teacher.HireDate,
                IsClassTeacher = teacher.IsClassTeacher,
                Status = teacher.Status,
                ClassId = teacher.ClassId,                
                SubjectsTaught = teacher.ClassSubjects?.Select(cs => cs.Subject.Name).ToList() ?? new List<string>()
            };
        }
    }
}
