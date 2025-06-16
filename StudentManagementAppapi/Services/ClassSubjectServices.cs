using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IClassSubjectRepository _repository;

        public ClassSubjectService(IClassSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseWrapper<ClassSubjectDto>> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result == null
                ? ResponseWrapper<ClassSubjectDto>.Failure("ClassSubject not found")
                : ResponseWrapper<ClassSubjectDto>.Success(MapToDto(result));
        }

        public async Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            return ResponseWrapper<IEnumerable<ClassSubjectDto>>.Success(results.Select(MapToDto));
        }

        public async Task<ResponseWrapper<ClassSubjectDto>> CreateAsync(CreateClassSubjectDto dto)
        {
            var classSubject = new ClassSubject
            {
                ClassId = dto.ClassId,
                SubjectId = dto.SubjectId,
                TeacherId = dto.TeacherId
            };

            var created = await _repository.AddAsync(classSubject);
            return ResponseWrapper<ClassSubjectDto>.Success(MapToDto(created), "ClassSubject created successfully");
        }

        public async Task<ResponseWrapper<ClassSubjectDto>> UpdateAsync(Guid id, CreateClassSubjectDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return ResponseWrapper<ClassSubjectDto>.Failure("ClassSubject not found");

            existing.ClassId = dto.ClassId;
            existing.SubjectId = dto.SubjectId;
            existing.TeacherId = dto.TeacherId;

            var updated = await _repository.UpdateAsync(existing);
            return ResponseWrapper<ClassSubjectDto>.Success(MapToDto(updated), "ClassSubject updated successfully");
        }

        public async Task<ResponseWrapper<bool>> DeleteAsync(Guid id)
        {
            var success = await _repository.DeleteAsync(id);
            return success
                ? ResponseWrapper<bool>.Success(true, "ClassSubject deleted successfully")
                : ResponseWrapper<bool>.Failure("ClassSubject not found or already deleted");
        }

        public async Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetByClassIdAsync(Guid classId)
        {
            var results = await _repository.GetByClassIdAsync(classId);
            return ResponseWrapper<IEnumerable<ClassSubjectDto>>.Success(results.Select(MapToDto));
        }

        public async Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetBySubjectIdAsync(Guid subjectId)
        {
            var results = await _repository.GetBySubjectIdAsync(subjectId);
            return ResponseWrapper<IEnumerable<ClassSubjectDto>>.Success(results.Select(MapToDto));
        }

        public async Task<ResponseWrapper<IEnumerable<ClassSubjectDto>>> GetByTeacherIdAsync(Guid teacherId)
        {
            var results = await _repository.GetByTeacherIdAsync(teacherId);
            return ResponseWrapper<IEnumerable<ClassSubjectDto>>.Success(results.Select(MapToDto));
        }

        public async Task<ResponseWrapper<ClassSubjectDto>> GetByClassAndSubjectAsync(Guid classId, Guid subjectId)
        {
            var result = await _repository.GetByClassAndSubjectAsync(classId, subjectId);
            return result == null
                ? ResponseWrapper<ClassSubjectDto>.Failure("Not found")
                : ResponseWrapper<ClassSubjectDto>.Success(MapToDto(result));
        }

        private ClassSubjectDto MapToDto(ClassSubject cs)
        {
            return new ClassSubjectDto
            {
                Id = cs.Id,
                ClassId = cs.ClassId,
                ClassName = cs.Class?.Name,

                SubjectId = cs.SubjectId,
                SubjectName = cs.Subject?.Name,

                TeacherId = cs.TeacherId,
                TeacherName = cs.Teacher?.User != null
                    ? $"{cs.Teacher.User.FirstName} {cs.Teacher.User.LastName}"
                    : null,

                PeriodsPerWeek = cs.PeriodsPerWeek
            };
        }

    }

}
