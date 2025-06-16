using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IStudentRepository _studentRepository;

        public ResultService(IResultRepository resultRepository, IStudentRepository studentRepository)
        {
            _resultRepository = resultRepository;
            _studentRepository = studentRepository;
        }

        public async Task<ResponseWrapper<ResultResponse>> CreateResultAsync(CreateResultDto createResultDto)
        {
            try
            {
                var result = new Result
                {
                    StudentId = createResultDto.StudentId,
                    AssessmentId = createResultDto.AssessmentId,
                    Score = createResultDto.Score,
                    Remarks = createResultDto.Remarks,
                    RecordedDate = DateTime.Now
                };

                var createdResult = await _resultRepository.AddAsync(result);
                var resultDto = await MapToResultDto(createdResult);

                return new ResponseWrapper<ResultResponse>
                {
                    Successs = true,
                    Message = "Result created successfully",
                    Data = resultDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<ResultResponse>
                {
                    Successs = false,
                    Message = $"Error creating result: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<ResultResponse>>> GetResultsByStudentIdAsync(string admissionNo)
        {
            try
            {
                var results = await _resultRepository.GetByStudentIdAsync(admissionNo);
                var resultDtos = new List<ResultResponse>();

                foreach (var result in results)
                {
                    resultDtos.Add(await MapToResultDto(result));
                }

                return new ResponseWrapper<IEnumerable<ResultResponse>>
                {
                    Successs = true,
                    Data = resultDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<ResultResponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving results: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<IEnumerable<ResultResponse>>> GetResultsByAssessmentIdAsync(Guid assessmentId)
        {
            try
            {
                var results = await _resultRepository.GetByAssessmentIdAsync(assessmentId);
                var resultDtos = new List<ResultResponse>();

                foreach (var result in results)
                {
                    resultDtos.Add(await MapToResultDto(result));
                }

                return new ResponseWrapper<IEnumerable<ResultResponse>>
                {
                    Successs = true,
                    Data = resultDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<ResultResponse>>
                {
                    Successs = false,
                    Message = $"Error retrieving results: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<ResultResponse>> UpdateResultAsync(Guid id, CreateResultDto updateDto)
        {
            try
            {
                var result = await _resultRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return new ResponseWrapper<ResultResponse>
                    {
                        Successs = false,
                        Message = "Result not found"
                    };
                }

                result.Score = updateDto.Score;
               
                result.Remarks = updateDto.Remarks;
                result.UpdatedAt = DateTime.Now;

                var updatedResult = await _resultRepository.UpdateAsync(result);
                var resultDto = await MapToResultDto(updatedResult);

                return new ResponseWrapper<ResultResponse>
                {
                    Successs = true,
                    Message = "Result updated successfully",
                    Data = resultDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<ResultResponse>
                {
                    Successs = false,
                    Message = $"Error updating result: {ex.Message}"
                };
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteResultAsync(Guid id)
        {
            try
            {
                var exists = await _resultRepository.ExistsAsync(id);
                if (!exists)
                {
                    return new ResponseWrapper<bool>
                    {
                        Successs = false,
                        Message = "Result not found"
                    };
                }

                await _resultRepository.DeleteAsync(id);
                return new ResponseWrapper<bool>
                {
                    Successs = true,
                    Message = "Result deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool>
                {
                    Successs = false,
                    Message = $"Error deleting result: {ex.Message}"
                };
            }
        }

        private async Task<ResultResponse> MapToResultDto(Result result)
        {
            var student = await _studentRepository.GetWithUserAsync(result.StudentId);

            return new ResultResponse
            {
                Id = result.Id,
                StudentId = result.StudentId,
                StudentName = student != null ? $"{student.User.FirstName} {student.User.LastName}" : "",
                AssessmentId = result.AssessmentId,
                AssessmentTitle = result.Assessment.AssessmentType ,
                SubjectName = result.Assessment?.ClassSubject?.Subject?.Name ?? "",
                Score = result.Score,
                
                Remarks = result.Remarks,
                RecordedDate = result.RecordedDate
            };
        }
    }


}
