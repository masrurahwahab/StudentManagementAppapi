using AutoMapper;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class AcademicTermService : IAcademicTermService
    {
        private readonly IAcademicTermRepository _termRepository;

        public AcademicTermService(IAcademicTermRepository termRepository)
        {
            _termRepository = termRepository;
        }

        public async Task<ResponseWrapper<AcademicTermDto>> CreateTermAsync(CreateAcademicTermDto createTermDto)
        {
            try
            {
                var term = new AcademicTerm
                {
                    Name = createTermDto.Name,
                    StartDate = createTermDto.StartDate,
                    EndDate = createTermDto.EndDate,
                    IsCurrent = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _termRepository.AddAsync(term);

                var termDto = MapToDto(term);
                return ResponseWrapper<AcademicTermDto>.Success(termDto, "Academic term created successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AcademicTermDto>.Failure($"Error creating term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AcademicTermDto>> GetTermByIdAsync(Guid id)
        {
            try
            {
                var term = await _termRepository.GetByIdAsync(id);
                if (term == null)
                    return ResponseWrapper<AcademicTermDto>.Failure("Academic term not found");

                var termDto = MapToDto(term);
                return ResponseWrapper<AcademicTermDto>.Success(termDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AcademicTermDto>.Failure($"Error retrieving term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<IEnumerable<AcademicTermDto>>> GetAllTermsAsync()
        {
            try
            {
                var terms = await _termRepository.GetAllAsync();
                var termDtos = terms.Select(MapToDto);
                return ResponseWrapper<IEnumerable<AcademicTermDto>>.Success(termDtos);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<IEnumerable<AcademicTermDto>>.Failure($"Error retrieving terms: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AcademicTermDto>> UpdateTermAsync(Guid id, CreateAcademicTermDto updateDto)
        {
            try
            {
                var term = await _termRepository.GetByIdAsync(id);
                if (term == null)
                    return ResponseWrapper<AcademicTermDto>.Failure("Academic term not found");

                // Manual update
                term.Name = updateDto.Name;
                term.StartDate = updateDto.StartDate;
                term.EndDate = updateDto.EndDate;
                term.UpdatedAt = DateTime.UtcNow;

                await _termRepository.UpdateAsync(term);

                var termDto = MapToDto(term);
                return ResponseWrapper<AcademicTermDto>.Success(termDto, "Academic term updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AcademicTermDto>.Failure($"Error updating term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> DeleteTermAsync(Guid id)
        {
            try
            {
                var term = await _termRepository.GetByIdAsync(id);
                if (term == null)
                    return ResponseWrapper<bool>.Failure("Academic term not found");

                await _termRepository.DeleteAsync(id);
                return ResponseWrapper<bool>.Success(true, "Academic term deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error deleting term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<AcademicTermDto>> GetCurrentTermAsync()
        {
            try
            {
                var term = await _termRepository.GetCurrentTermAsync();
                if (term == null)
                    return ResponseWrapper<AcademicTermDto>.Failure("No current term found");

                var termDto = MapToDto(term);
                return ResponseWrapper<AcademicTermDto>.Success(termDto);
            }
            catch (Exception ex)
            {
                return ResponseWrapper<AcademicTermDto>.Failure($"Error retrieving current term: {ex.Message}");
            }
        }

        public async Task<ResponseWrapper<bool>> SetCurrentTermAsync(Guid termId)
        {
            try
            {
                var result = await _termRepository.SetCurrentTermAsync(termId);
                if (!result)
                    return ResponseWrapper<bool>.Failure("Term not found");

                return ResponseWrapper<bool>.Success(true, "Current term set successfully");
            }
            catch (Exception ex)
            {
                return ResponseWrapper<bool>.Failure($"Error setting current term: {ex.Message}");
            }
        }

       
        private AcademicTermDto MapToDto(AcademicTerm term)
        {
            return new AcademicTermDto
            {
                Id = term.Id,
                Name = term.Name,
                AcademicYear = term.AcademicYear,
                StartDate = term.StartDate,
                EndDate = term.EndDate,
                IsCurrent = term.IsCurrent,
                CreatedAt = term.CreatedAt,
                UpdatedAt = term.UpdatedAt
            };
        }
    }


}
