using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using StudentManagementAppapi.PasswordValidation;

namespace StudentManagementAppapi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassRepository _classRepository;
        private readonly IPasswordHashing _password;

        public StudentService(IStudentRepository studentRepository, IUserRepository userRepository, IClassRepository classRepository,
            IPasswordHashing password)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _password = password;
            _classRepository = classRepository;
        }

        public async Task<ResponseWrapper<StudentDto>> CreateStudentAsync(CreateStudent createStudentDto)
        {
            try
            {
                var canAddStudent = await _classRepository.CanAddStudentAsync(createStudentDto.ClassId);
                if (!canAddStudent)
                {
                    return new ResponseWrapper<StudentDto>
                    {
                        Successs = false,
                        Message = "Class is already full"
                    };
                }

                var salt = _password.GenerateSalt();
                var passwordHash = _password.HashPassword(createStudentDto.User.Password, salt);

                var user = new User
                {
                    Username = createStudentDto.User.UserName,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    Email = createStudentDto.User.Email,
                    FirstName = createStudentDto.User.FirstName,
                    LastName = createStudentDto.User.LastName,
                    Role = UserRole.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);

                var admissionNumber = await GenerateAdmissionNumberAsync();

                var student = new Student
                {
                    UserId = user.Id,
                    AdmissionNumber = admissionNumber,
                    DateOfBirth = createStudentDto.DateOfBirth,
                    Gender = createStudentDto.Gender,
                    Address = createStudentDto.Address,
                    ParentPhone = createStudentDto.ParentPhone,
                    ParentEmail = createStudentDto.ParentEmail,
                    EmergencyContact = createStudentDto.EmergencyContact,
                    ClassId = createStudentDto.ClassId,
                    AdmissionDate = createStudentDto.AdmissionDate,
                    Status = StudentStatus.Active
                };

                var createdStudent = await _studentRepository.AddAsync(student);
                var studentDto = await MapToStudentDto(createdStudent);

                return new ResponseWrapper<StudentDto>
                {
                    Successs = true, 
                    Message = "Student created successfully",
                    Data = studentDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<StudentDto>
                {
                    Successs = false,
                    Message = $"Error creating student: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<StudentDto>> GetStudentByIdAsync(Guid id) 
        {
            try
            {
                var student = await _studentRepository.GetWithUserAsync(id);
                if (student == null)
                {
                    return new ResponseWrapper<StudentDto>
                    {
                        Successs = false,
                        Message = "Student not found"
                    };
                }

                var studentDto = await MapToStudentDto(student);
                return new ResponseWrapper<StudentDto> 
                {
                    Successs = true,
                    Data = studentDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<StudentDto>
                {
                    Successs = false,
                    Message = $"Error retrieving student: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<StudentDto>>> GetAllStudentsAsync() 
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();
                var studentDtos = new List<StudentDto>();

                foreach (var student in students)
                {
                    var studentWithUser = await _studentRepository.GetWithUserAsync(student.Id);
                    studentDtos.Add(await MapToStudentDto(studentWithUser));
                }

                return new ResponseWrapper<IEnumerable<StudentDto>>
                {
                    Successs = true,
                    Data = studentDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<StudentDto>>
                {
                    Successs = false,
                    Message = $"Error retrieving students: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<PagedResponse<StudentDto>>> GetPagedStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null) 
        {
            try
            {
                var pagedStudents = await _studentRepository.GetPagedAsync(pageNumber, pageSize, searchTerm);
                var studentDtos = new List<StudentDto>();

                foreach (var student in pagedStudents.Data)
                {
                    studentDtos.Add(await MapToStudentDto(student));
                }

                var pagedResponse = new PagedResponse<StudentDto>
                {
                    Data = studentDtos,
                    PageNumber = pagedStudents.PageNumber,
                    PageSize = pagedStudents.PageSize,
                    TotalRecords = pagedStudents.TotalRecords,
                    TotalPages = pagedStudents.TotalPages
                };

                return new ResponseWrapper<PagedResponse<StudentDto>>
                {
                    Successs = true,
                    Data = pagedResponse
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<PagedResponse<StudentDto>> 
                {
                    Successs = false,
                    Message = $"Error retrieving students: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<StudentDto>>> GetStudentsByClassAsync(Guid classId) 
        {
            try
            {
                var students = await _studentRepository.GetByClassIdAsync(classId);
                var studentDtos = new List<StudentDto>();

                foreach (var student in students)
                {
                    studentDtos.Add(await MapToStudentDto(student));
                }

                return new ResponseWrapper<IEnumerable<StudentDto>> 
                {
                    Successs = true,
                    Data = studentDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<StudentDto>>
                {
                    Successs = false,
                    Message = $"Error retrieving students: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<StudentDto>> UpdateStudentAsync(Guid id, CreateStudent updateDto) 
        {
            try
            {
                var student = await _studentRepository.GetWithUserAsync(id);
                if (student == null)
                {
                    return new ResponseWrapper<StudentDto>
                    {
                        Successs = false,
                        Message = "Student not found"
                    };
                }

              
                student.User.FirstName = updateDto.User.FirstName;
                student.User.LastName = updateDto.User.LastName;
                student.User.Email = updateDto.User.Email;
               

                
                student.DateOfBirth = updateDto.DateOfBirth;
                student.Gender = updateDto.Gender;
                student.Address = updateDto.Address;
                student.ParentPhone = updateDto.ParentPhone;
                student.ParentEmail = updateDto.ParentEmail;
                student.EmergencyContact = updateDto.EmergencyContact;
                

                var updatedStudent = await _studentRepository.UpdateAsync(student);
                var studentDto = await MapToStudentDto(updatedStudent);

                return new ResponseWrapper<StudentDto>
                {
                    Successs = true,
                    Message = "Student updated successfully",
                    Data = studentDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<StudentDto>
                {
                    Successs = false,
                    Message = $"Error updating student: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteStudentAsync(Guid id) 
        {
            try
            {
                var exists = await _studentRepository.ExistsAsync(id);
                if (!exists)
                {
                    return new ResponseWrapper<bool> 
                    {
                        Successs = false,
                        Message = "Student not found"
                    };
                }

                await _studentRepository.DeleteAsync(id);
                return new ResponseWrapper<bool>
                {
                    Successs = true,
                    Message = "Student deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool> 
                {
                    Successs = false,
                    Message = $"Error deleting student: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<StudentDto>> GetByAdmissionNumberAsync(string admissionNumber) 
        {
            try
            {
                var student = await _studentRepository.GetByAdmissionNumberAsync(admissionNumber);
                if (student == null)
                {
                    return new ResponseWrapper<StudentDto>
                    {
                        Successs = false,
                        Message = "Student not found"
                    };
                }

                var studentDto = await MapToStudentDto(student);
                return new ResponseWrapper<StudentDto>
                {
                    Successs = true,
                    Data = studentDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<StudentDto> 
                {
                    Successs = false,
                    Message = $"Error retrieving student: {ex.Message}"
                };
            }
        }

        private async Task<StudentDto> MapToStudentDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                User = new UserResponseModel
                {
                    Id = student.User.Id,
                    Username = student.User.Username,
                    Email = student.User.Email,
                    Role = student.User.Role,
                    FirstName = student.User.FirstName,
                    LastName = student.User.LastName,
                    IsActive = student.User.IsActive,
                    CreatedAt = student.User.CreatedAt
                },
                AdmissionNumber = student.AdmissionNumber,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                ParentPhone = student.ParentPhone,
                ParentEmail = student.ParentEmail,
                EmergencyContact = student.EmergencyContact,
                ClassId = student.ClassId,
                ClassName = student.Class?.Name ?? "",
                AdmissionDate = student.AdmissionDate,
                Status = student.Status,
            };
        }

        private async Task<string> GenerateAdmissionNumberAsync()
        {
            var year = DateTime.Now.Year;
            var count = await _studentRepository.GetCountAsync();
            return $"{year}{(count + 1):D4}";
        }
    }

}
