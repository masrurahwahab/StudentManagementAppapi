using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using System.Diagnostics.Contracts;

namespace StudentManagementAppapi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ResponseWrapper<SubjectDto>> CreateSubjectAsync(CreateSubjectDto createSubjectDto)
        {
            try
            {
                var generatedCode = GenerateUniqueSubjectCode(createSubjectDto.Name);

                
                while (await _subjectRepository.GetByCodeAsync(generatedCode) != null)
                {
                    generatedCode = GenerateUniqueSubjectCode(createSubjectDto.Name);
                }               

                var subject = new Subject
                {
                    Name = createSubjectDto.Name,
                    Code = generatedCode,
                    IsCore = createSubjectDto.IsCore,
                    ClassLevel = createSubjectDto.ClassLevel
                };

                await _subjectRepository.AddAsync(subject);

                var subjectDto = MapToDto(subject);
                return ResponseWrapper<SubjectDto>.Success(subjectDto, "Subject created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<SubjectDto>.Failure($"Error creating subject: {ex.Message}");
            }
        }


        private string GenerateUniqueSubjectCode(string subjectName)
        {
            var baseCode = new string(subjectName.Where(char.IsLetter).Take(3).ToArray()).ToUpper();
            var uniqueSuffix = DateTime.UtcNow.Ticks % 10000;
            return $"{baseCode}{uniqueSuffix}";
        }

        public async Task<ResponseWrapper<SubjectDto>> GetSubjectByIdAsync(Guid id)
        {
            try
            {
                var subject = await _subjectRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    return ResponseWrapper<SubjectDto>.Failure("Subject not found");
                }

                var subjectDto = MapToDto(subject);
                return ResponseWrapper<SubjectDto>.Success(subjectDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<SubjectDto>.Failure($"Error retrieving subject: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<SubjectDto>>> GetAllSubjectsAsync()
        {
            try
            {
                var subjects = await _subjectRepository.GetAllAsync();
                var subjectDtos = subjects.Select(MapToDto);
                return ResponseWrapper<IEnumerable<SubjectDto>>.Success(subjectDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<SubjectDto>>.Failure($"Error retrieving subjects: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<SubjectDto>> UpdateSubjectAsync(Guid id, CreateSubjectDto updateDto)
        {
            try
            {
                var generatedCode = GenerateUniqueSubjectCode(updateDto.Name);

               
                while (await _subjectRepository.GetByCodeAsync(generatedCode) != null)
                {
                    generatedCode = GenerateUniqueSubjectCode(updateDto.Name);
                }
                var subject = await _subjectRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    return ResponseWrapper<SubjectDto>.Failure("Subject not found");
                }

                subject.Name = updateDto.Name;
                subject.Code = generatedCode;
                subject.IsCore = updateDto.IsCore;
                subject.ClassLevel = updateDto.ClassLevel;

                await _subjectRepository.UpdateAsync(subject);

                var subjectDto = MapToDto(subject);
                return ResponseWrapper<SubjectDto>.Success(subjectDto, "Subject updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<SubjectDto>.Failure($"Error updating subject: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteSubjectAsync(Guid id)
        {
            try
            {
                var subject = await _subjectRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    return ResponseWrapper<bool>.Failure("Subject not found");
                }

                await _subjectRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Subject deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting subject: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<SubjectDto>>> GetSubjectsByClassLevelAsync(ClassLevel classLevel)
        {
            try
            {
                var subjects = await _subjectRepository.GetByClassLevelAsync(classLevel);
                var subjectDtos = subjects.Select(MapToDto);
                return ResponseWrapper<IEnumerable<SubjectDto>>.Success(subjectDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<SubjectDto>>.Failure($"Error retrieving subjects: {ex.Message}");
            }
        }

        private SubjectDto MapToDto(Subject subject)
        {
            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                IsCore = subject.IsCore,
                ClassLevel = subject.ClassLevel,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}

