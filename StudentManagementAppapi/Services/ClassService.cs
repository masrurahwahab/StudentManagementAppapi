using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ClassService(IClassRepository classRepository, ITeacherRepository teacherRepository)
        {
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
        }

        //public async Task<ResponseWrapper<Classresponse>> CreateClassAsync(CreateClassDto createClassDto)
        //{
        //    try
        //    {
        //        var classEntity = new Class
        //        {
        //            Name = createClassDto.Name,
        //            Level = createClassDto.Level,
        //            ClassTeacherId = createClassDto.ClassTeacherId,
        //            AcademicYear = createClassDto.AcademicYear,
        //            MaxStudents = createClassDto.MaxStudents
        //        };

        //        var createdClass = await _classRepository.AddAsync(classEntity);
        //        var classDto = await MapToClassDto(createdClass);

        //        return new ResponseWrapper<Classresponse>
        //        {
        //            Successs = true,
        //            Message = "Class created successfully",
        //            Data = classDto
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseWrapper<Classresponse>
        //        {
        //            Successs = false,
        //            Message = $"Error creating class: {ex.Message}"
        //        };
        //    }
        //}

        public async Task<ResponseWrapper<Classresponse>> CreateClassAsync(CreateClassDto createClassDto)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(createClassDto.Name))
                {
                    return new ResponseWrapper<Classresponse>
                    {
                        Successs = false,
                        Message = "Class name is required"
                    };
                }

                
                if (createClassDto.ClassTeacherId.HasValue)
                {
                    var teacherExists = await _teacherRepository.ExistsAsync(createClassDto.ClassTeacherId.Value);                       

                    if (!teacherExists)
                    {
                        return new ResponseWrapper<Classresponse>
                        {
                            Successs = false,
                            Message = $"Teacher with ID {createClassDto.ClassTeacherId} does not exist"
                        };
                    }
                }               

                var classEntity = new Class
                {
                    Name = createClassDto.Name,
                    Level = createClassDto.Level,
                    ClassTeacherId = createClassDto.ClassTeacherId,
                    AcademicYear = createClassDto.AcademicYear,
                    MaxStudents = createClassDto.MaxStudents,
                    CreatedAt = DateTime.UtcNow, 
                    
                };

                var createdClass = await _classRepository.AddAsync(classEntity);

               

                var classDto = await MapToClassDto(createdClass);

                return new ResponseWrapper<Classresponse>
                {
                    Successs = true,
                    Message = "Class created successfully",
                    Data = classDto
                };
            }
            catch (Exception ex)
            {
               
                var innerError = ex.InnerException?.Message ?? ex.Message;
                return new ResponseWrapper<Classresponse>
                {
                    Successs = false,
                    Message = $"Error creating class: {innerError}"
                };
            }
        }

        public async Task<ResponseWrapper<Classresponse>> GetClassByIdAsync(Guid id)
        {
            try
            {
                var classEntity = await _classRepository.GetByIdAsync(id);
                if (classEntity == null)
                {
                    return new ResponseWrapper<Classresponse>
                    {
                        Successs = false,
                        Message = "Class not found"
                    };
                }

                var classDto = await MapToClassDto(classEntity);
                return new ResponseWrapper<Classresponse>
                {
                    Successs = true,
                    Data = classDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<Classresponse>
                {
                    Successs = false,
                    Message = $"Error retrieving class: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<Classresponse>>> GetAllClassesAsync()
        {
            try
            {
                var classes = await _classRepository.GetAllAsync();
                var classDtos = new List<Classresponse>();

                foreach (var classEntity in classes)
                {
                    classDtos.Add(await MapToClassDto(classEntity));
                }

                return new ResponseWrapper<IEnumerable<Classresponse>>
                {
                    Successs = true,
                    Data = classDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<Classresponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving classes: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<Classresponse>>> GetClassesByAcademicYearAsync(string academicYear)
        {
            try
            {
                var classes = await _classRepository.GetByAcademicYearAsync(academicYear);
                var classDtos = new List<Classresponse>();

                foreach (var classEntity in classes)
                {
                    classDtos.Add(await MapToClassDto(classEntity));
                }

                return new ResponseWrapper<IEnumerable<Classresponse>>
                {
                    Successs = true,
                    Data = classDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<Classresponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving classes: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<Classresponse>> UpdateClassAsync(Guid id, CreateClassDto updateDto)
        {
            try
            {
                var classEntity = await _classRepository.GetByIdAsync(id);
                if (classEntity == null)
                {
                    return new ResponseWrapper<Classresponse>
                    {
                        Successs = false,
                        Message = "Class not found"
                    };
                }

                classEntity.Name = updateDto.Name;
                classEntity.Level = updateDto.Level;
                classEntity.ClassTeacherId = updateDto.ClassTeacherId;
                classEntity.AcademicYear = updateDto.AcademicYear;
                classEntity.MaxStudents = updateDto.MaxStudents;

                var updatedClass = await _classRepository.UpdateAsync(classEntity);
                var classDto = await MapToClassDto(updatedClass);

                return new ResponseWrapper<Classresponse>
                {
                    Successs = true,
                    Message = "Class updated successfully",
                    Data = classDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<Classresponse>
                {
                    Successs = false,
                    Message = $"Error updating class: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteClassAsync(Guid id)
        {
            try
            {
                var exists = await _classRepository.ExistsAsync(id);
                if (!exists)
                {
                    return new ResponseWrapper<bool>
                    {
                        Successs = false,
                        Message = "Class not found"
                    };
                }

                await _classRepository.DeleteAsync(id);
                return new ResponseWrapper<bool>
                {
                    Successs = true,
                    Message = "Class deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool>
                {
                    Successs = false,
                    Message = $"Error deleting class: {ex.Message}"
                };
            }
        }

        private async Task<Classresponse> MapToClassDto(Class classEntity)
        {
            var classTeacher = classEntity.ClassTeacherId.HasValue
                ? await _teacherRepository.GetByUserIdAsync(classEntity.ClassTeacherId.Value)
                : null;

            return new Classresponse
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                Level = classEntity.Level,
                ClassTeacherId = classEntity.ClassTeacherId,
                ClassTeacherName = classTeacher != null ? $"{classTeacher.User.FirstName} {classTeacher.User.LastName}" : "",
                AcademicYear = classEntity.AcademicYear,
                MaxStudents = classEntity.MaxStudents,
                CurrentStudentsCount = classEntity.CurrentStudentsCount
            };
        }
    }

}
