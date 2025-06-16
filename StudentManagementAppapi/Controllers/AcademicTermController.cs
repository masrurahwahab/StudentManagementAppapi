using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AcademicTermController : ControllerBase
    {
        private readonly IAcademicTermService _academicTermService;

        public AcademicTermController(IAcademicTermService academicTermService)
        {
            _academicTermService = academicTermService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTerm([FromBody] CreateAcademicTermDto createTermDto)
        {
            var result = await _academicTermService.CreateTermAsync(createTermDto);

            if (result.Successs)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTermById(Guid id)
        {
            var result = await _academicTermService.GetTermByIdAsync(id);

            if (result.Successs)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTerms()
        {
            var result = await _academicTermService.GetAllTermsAsync();

            if (result.Successs)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTerm(Guid id, [FromBody] CreateAcademicTermDto updateDto)
        {
            var result = await _academicTermService.UpdateTermAsync(id, updateDto);

            if (result.Successs)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerm(Guid id)
        {
            var result = await _academicTermService.DeleteTermAsync(id);

            if (result.Successs)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentTerm()
        {
            var result = await _academicTermService.GetCurrentTermAsync();

            if (result.Successs)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPut("{id}/set-current")]
        public async Task<IActionResult> SetCurrentTerm(Guid id)
        {
            var result = await _academicTermService.SetCurrentTermAsync(id);

            if (result.Successs)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
