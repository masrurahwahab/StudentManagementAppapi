using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;


namespace StudentManagementAppapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult([FromBody] CreateResultDto dto)
        {
            if (dto == null || dto.StudentId == Guid.Empty || dto.AssessmentId == Guid.Empty)
            {
                return BadRequest("Invalid result data.");
            }

            var response = await _resultService.CreateResultAsync(dto);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("student/{admissionNo}")]
        public async Task<IActionResult> GetResultsByStudentId(string admissionNo)
        {
            if (string.IsNullOrWhiteSpace(admissionNo))
            {
                return BadRequest("Admission number is required.");
            }

            var response = await _resultService.GetResultsByStudentIdAsync(admissionNo);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("assessment/{assessmentId:guid}")]
        public async Task<IActionResult> GetResultsByAssessmentId(Guid assessmentId)
        {
            if (assessmentId == Guid.Empty)
            {
                return BadRequest("Invalid assessment ID.");
            }

            var response = await _resultService.GetResultsByAssessmentIdAsync(assessmentId);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateResult(Guid id, [FromBody] CreateResultDto dto)
        {
            if (id == Guid.Empty || dto == null)
            {
                return BadRequest("Invalid update request.");
            }

            var response = await _resultService.UpdateResultAsync(id, dto);
            if (!response.Successs)
            {
                if (response.Message == "Result not found")
                    return NotFound(response);

                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteResult(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid result ID.");
            }

            var response = await _resultService.DeleteResultAsync(id);
            if (!response.Successs)
            {
                if (response.Message == "Result not found")
                    return NotFound(response);

                return StatusCode(500, response);
            }

            return Ok(response);
        }
    }

}
