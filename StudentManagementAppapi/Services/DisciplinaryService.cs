using AutoMapper;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Services
{
    public class DisciplinaryService : IDisciplinaryService
    {
        private readonly IDisciplinaryRepository _disciplinaryRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public DisciplinaryService(
            IDisciplinaryRepository disciplinaryRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _disciplinaryRepository = disciplinaryRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<ResponseWrapper<DisciplinaryDto>> CreateDisciplinaryRecordAsync(CreateDisciplinaryDto createDto)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(createDto.StudentId);
                if (student == null)
                    return ResponseWrapper<DisciplinaryDto>.Failure("Student not found");

                var reporter = await _teacherRepository.GetByIdAsync(createDto.ReportedById);
                if (reporter == null)
                    return ResponseWrapper<DisciplinaryDto>.Failure("Reporter not found");

                var disciplinary = new Disciplinary
                {
                    Id = Guid.NewGuid(),
                    StudentId = createDto.StudentId,
                    ReportedById = createDto.ReportedById,
                    Description = createDto.Description,
                    ActionTaken = createDto.ActionTaken,
                    DateReported = DateTime.UtcNow,
                    Status = DisciplinaryStatus.Pending
                };

                await _disciplinaryRepository.AddAsync(disciplinary);

                var dto = MapToDto(disciplinary);
                return ResponseWrapper<DisciplinaryDto>.Success(dto, "Disciplinary record created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<DisciplinaryDto>.Failure($"Error creating disciplinary record: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<DisciplinaryDto>> GetDisciplinaryRecordByIdAsync(Guid id)
        {
            try
            {
                var record = await _disciplinaryRepository.GetByIdAsync(id);
                if (record == null)
                    return ResponseWrapper<DisciplinaryDto>.Failure("Disciplinary record not found");

                var dto = MapToDto(record);
                return ResponseWrapper<DisciplinaryDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<DisciplinaryDto>.Failure($"Error retrieving disciplinary record: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetAllDisciplinaryRecordsAsync()
        {
            try
            {
                var records = await _disciplinaryRepository.GetAllAsync();
                var dtos = records.Select(MapToDto);
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Failure($"Error retrieving disciplinary records: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<DisciplinaryDto>> UpdateDisciplinaryRecordAsync(Guid id, CreateDisciplinaryDto updateDto)
        {
            try
            {
                var record = await _disciplinaryRepository.GetByIdAsync(id);
                if (record == null)
                    return ResponseWrapper<DisciplinaryDto>.Failure("Disciplinary record not found");

                record.Description = updateDto.Description;
                record.ActionTaken = updateDto.ActionTaken;
                record.StudentId = updateDto.StudentId;
                record.ReportedById = updateDto.ReportedById;

                await _disciplinaryRepository.UpdateAsync(record);

                var dto = MapToDto(record);
                return ResponseWrapper<DisciplinaryDto>.Success(dto, "Disciplinary record updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<DisciplinaryDto>.Failure($"Error updating disciplinary record: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteDisciplinaryRecordAsync(Guid id)
        {
            try
            {
                var record = await _disciplinaryRepository.GetByIdAsync(id);
                if (record == null)
                    return ResponseWrapper<bool>.Failure("Disciplinary record not found");

                await _disciplinaryRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Disciplinary record deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting disciplinary record: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetDisciplinaryRecordsByStudentAsync(Guid studentId)
        {
            try
            {
                var records = await _disciplinaryRepository.GetByStudentIdAsync(studentId);
                var dtos = records.Select(MapToDto);
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Failure($"Error retrieving student disciplinary records: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<DisciplinaryDto>>> GetDisciplinaryRecordsByStatusAsync(DisciplinaryStatus status)
        {
            try
            {
                var records = await _disciplinaryRepository.GetByStatusAsync(status);
                var dtos = records.Select(MapToDto);
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<DisciplinaryDto>>.Failure($"Error retrieving disciplinary records by status: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> UpdateDisciplinaryStatusAsync(Guid id, DisciplinaryStatus status)
        {
            try
            {
                var record = await _disciplinaryRepository.GetByIdAsync(id);
                if (record == null)
                    return ResponseWrapper<bool>.Failure("Disciplinary record not found");

                record.Status = status;
                await _disciplinaryRepository.UpdateAsync(record);

                return ResponseWrapper<bool>.Success(true, "Disciplinary status updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error updating disciplinary status: {ex.Message}");
            }
        }

        private DisciplinaryDto MapToDto(Disciplinary record)
        {
            return new DisciplinaryDto
            {
                Id = record.Id,
                StudentId = record.StudentId,
                ReportedById = record.ReportedById,
                Description = record.Description,
                ActionTaken = record.ActionTaken,
                DateReported = record.DateReported,
                Status = record.Status
            };
        }
    }

}
