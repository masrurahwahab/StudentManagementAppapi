using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.PasswordValidation;
using AutoMapper;

namespace StudentManagementAppapi.Services
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPasswordHashing _passwordHashing;

        public ParentService(
            IParentRepository parentRepository,
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            IPasswordHashing passwordHashing)
        {
            _parentRepository = parentRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _passwordHashing = passwordHashing;
        }

        public async Task<ResponseWrapper<ParentDto>> CreateParentAsync(CreateParentDto createParentDto)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(createParentDto.StudentId);
                if (student == null)
                {
                    return ResponseWrapper<ParentDto>.Failure("Student not found");
                }

                var salt = _passwordHashing.GenerateSalt();
                var passwordHash = _passwordHashing.HashPassword(createParentDto.Password, salt);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = createParentDto.Username,
                    PasswordHash = passwordHash,
                    Salt = salt,
                    Email = createParentDto.Email,
                    FirstName = createParentDto.FirstName,
                    LastName = createParentDto.LastName,
                    Role = UserRole.Parent,
                    IsActive = true
                };

                await _userRepository.AddAsync(user);

                var parent = new Parent
                {
                    Id = Guid.NewGuid(),
                    Phone = createParentDto.Phone,
                    UserId = user.Id,
                    User = user,
                    Occupation = createParentDto.Occupation,
                    RelationshipToStudent = createParentDto.RelationshipToStudent,
                    StudentId = createParentDto.StudentId
                };

                await _parentRepository.AddAsync(parent);

                var parentDto = MapToDto(parent);
                return ResponseWrapper<ParentDto>.Success(parentDto, "Parent created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<ParentDto>.Failure($"Error creating parent: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<ParentDto>> GetParentByIdAsync(Guid id)
        {
            try
            {
                var parent = await _parentRepository.GetByIdAsync(id);
                if (parent == null)
                {
                    return ResponseWrapper<ParentDto>.Failure("Parent not found");
                }

                var parentDto = MapToDto(parent);
                return ResponseWrapper<ParentDto>.Success(parentDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<ParentDto>.Failure($"Error retrieving parent: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<ParentDto>>> GetAllParentsAsync()
        {
            try
            {
                var parents = await _parentRepository.GetAllAsync();
                var parentDtos = parents.Select(MapToDto);
                return ResponseWrapper<IEnumerable<ParentDto>>.Success(parentDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<ParentDto>>.Failure($"Error retrieving parents: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<ParentDto>> UpdateParentAsync(Guid id, CreateParentDto updateDto)
        {
            try
            {
                var parent = await _parentRepository.GetByIdAsync(id);
                if (parent == null)
                {
                    return ResponseWrapper<ParentDto>.Failure("Parent not found");
                }

                parent.Occupation = updateDto.Occupation;
                parent.RelationshipToStudent = updateDto.RelationshipToStudent;
                parent.Phone = updateDto.Phone;

                if (parent.User != null)
                {
                    parent.User.FirstName = updateDto.FirstName;
                    parent.User.LastName = updateDto.LastName;
                    parent.User.Email = updateDto.Email;
                }

                await _parentRepository.UpdateAsync(parent);

                var parentDto = MapToDto(parent);
                return ResponseWrapper<ParentDto>.Success(parentDto, "Parent updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<ParentDto>.Failure($"Error updating parent: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteParentAsync(Guid id)
        {
            try
            {
                var parent = await _parentRepository.GetByIdAsync(id);
                if (parent == null)
                {
                    return ResponseWrapper<bool>.Failure("Parent not found");
                }

                var deleted = await _parentRepository.DeleteAsync(id);
                if (deleted)
                {
                    return ResponseWrapper<bool>.Success(true, "Parent deleted successfully");
                }
                else
                {
                    return ResponseWrapper<bool>.Failure("Failed to delete parent");
                }
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting parent: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<ParentDto>> GetParentByStudentIdAsync(Guid studentId)
        {
            try
            {
                var parent = await _parentRepository.GetByStudentIdAsync(studentId);
                if (parent == null)
                {
                    return ResponseWrapper<ParentDto>.Failure("Parent not found for this student");
                }

                var parentDto = MapToDto(parent);
                return ResponseWrapper<ParentDto>.Success(parentDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<ParentDto>.Failure($"Error retrieving parent: {ex.Message}");
            }
        }

        private ParentDto MapToDto(Parent parent)
        {
            var user = new User();
            return new ParentDto
            {
                Id = parent.Id,              
                Occupation = parent.Occupation,
                RelationshipToStudent = parent.RelationshipToStudent,
                StudentId = parent.StudentId,
               
                User = new UserResponseModel
                {
                    Id = parent.User.Id,
                    Username = parent.User.Username,
                    Email = parent.User.Email,
                    Role = parent.User.Role,
                    FirstName = parent.User.FirstName,
                    LastName = parent.User.LastName,
                    IsActive = parent.User.IsActive,
                    CreatedAt = parent.User.CreatedAt
                },
                Phone = parent.Phone
            };
        }
    }

}
