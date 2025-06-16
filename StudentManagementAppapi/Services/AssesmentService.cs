
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IClassSubjectRepository _classSubjectRepository;
        private readonly IAcademicTermRepository _termRepository;

        public AssessmentService(
            IAssessmentRepository assessmentRepository,
            IClassSubjectRepository classSubjectRepository,
            IAcademicTermRepository termRepository)
        {
            _assessmentRepository = assessmentRepository;
            _classSubjectRepository = classSubjectRepository;
            _termRepository = termRepository;
        }

        public async Task<ResponseWrapper<AssessmentDto>> CreateAssessmentAsync(CreateAssessmentDto createAssessmentDto)
        {
            try
            {
                var classSubject = await _classSubjectRepository.GetByIdAsync(createAssessmentDto.ClassSubjectId);
                if (classSubject == null)
                    return ResponseWrapper<AssessmentDto>.Failure("Class subject not found");

                var term = await _termRepository.GetByIdAsync(createAssessmentDto.TermId);
                if (term == null)
                    return ResponseWrapper<AssessmentDto>.Failure("Academic term not found");

                var assessment = new Assessment
                {
                    Id = Guid.NewGuid(),
                    Title = createAssessmentDto.Title,                  
                    AssessmentType = createAssessmentDto.AssessmentType,
                    MaxScore = createAssessmentDto.MaxScore,
                    DateGiven = createAssessmentDto.DateGiven,
                    DueDate = createAssessmentDto.DueDate,
                    ClassSubjectId = createAssessmentDto.ClassSubjectId,
                    TermId = createAssessmentDto.TermId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _assessmentRepository.AddAsync(assessment);

                var assessmentDto = MapToDto(assessment);
                return ResponseWrapper<AssessmentDto>.Success(assessmentDto, "Assessment created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AssessmentDto>.Failure($"Error creating assessment: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AssessmentDto>> GetAssessmentByIdAsync(Guid id)
        {
            try
            {
                var assessment = await _assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                    return ResponseWrapper<AssessmentDto>.Failure("Assessment not found");

                var assessmentDto = MapToDto(assessment);
                return ResponseWrapper<AssessmentDto>.Success(assessmentDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AssessmentDto>.Failure($"Error retrieving assessment: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAllAssessmentsAsync()
        {
            try
            {
                var assessments = await _assessmentRepository.GetAllAsync();
                var assessmentDtos = assessments.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Success(assessmentDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Failure($"Error retrieving assessments: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AssessmentDto>> UpdateAssessmentAsync(Guid id, CreateAssessmentDto updateDto)
        {
            try
            {
                var assessment = await _assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                    return ResponseWrapper<AssessmentDto>.Failure("Assessment not found");

                assessment.Title = updateDto.Title;
                assessment.AssessmentType = updateDto.AssessmentType;
                assessment.MaxScore = updateDto.MaxScore;
                assessment.DateGiven = updateDto.DateGiven;
                assessment.DueDate = updateDto.DueDate;
                assessment.ClassSubjectId = updateDto.ClassSubjectId;
                assessment.TermId = updateDto.TermId;
                assessment.UpdatedAt = DateTime.UtcNow;

                await _assessmentRepository.UpdateAsync(assessment);

                var assessmentDto = MapToDto(assessment);
                return ResponseWrapper<AssessmentDto>.Success(assessmentDto, "Assessment updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AssessmentDto>.Failure($"Error updating assessment: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteAssessmentAsync(Guid id)
        {
            try
            {
                var assessment = await _assessmentRepository.GetByIdAsync(id);
                if (assessment == null)
                    return ResponseWrapper<bool>.Failure("Assessment not found");

                await _assessmentRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Assessment deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting assessment: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAssessmentsByClassAsync(Guid classId)
        {
            try
            {
                var assessments = await _assessmentRepository.GetByClassAsync(classId);
                var assessmentDtos = assessments.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Success(assessmentDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Failure($"Error retrieving assessments: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAssessmentsByTermAsync(Guid termId)
        {
            try
            {
                var assessments = await _assessmentRepository.GetByTermAsync(termId);
                var assessmentDtos = assessments.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Success(assessmentDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Failure($"Error retrieving assessments: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AssessmentDto>>> GetAssessmentsByTeacherAsync(Guid teacherId)
        {
            try
            {
                var assessments = await _assessmentRepository.GetByTeacherAsync(teacherId);
                var assessmentDtos = assessments.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Success(assessmentDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AssessmentDto>>.Failure($"Error retrieving assessments: {ex.Message}");
            }
        }

       
        private AssessmentDto MapToDto(Assessment assessment)
        {
            return new AssessmentDto
            {
                Id = assessment.Id,
                Title = assessment.Title,
                AssessmentType = assessment.AssessmentType,
                MaxScore = assessment.MaxScore,
                DateGiven = assessment.DateGiven,
                DueDate = assessment.DueDate,
                ClassSubjectId = assessment.ClassSubjectId,
                TermId = assessment.TermId,
                CreatedAt = assessment.CreatedAt,
                UpdatedAt = assessment.UpdatedAt
            };
        }
    }


}
