using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class TermReportsController : ControllerBase
    {
        private readonly ITermReportService _termReportService;

        public TermReportsController(ITermReportService termReportService)
        {
            _termReportService = termReportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTermReport([FromBody] CreateTermReportDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid input.");

            var result = await _termReportService.CreateTermReportAsync(dto);
            return result.Successs ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTermReportById(Guid id)
        {
            var result = await _termReportService.GetTermReportByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTermReports()
        {
            var result = await _termReportService.GetAllTermReportsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTermReport(Guid id, [FromBody] CreateTermReportDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid input.");

            var result = await _termReportService.UpdateTermReportAsync(id, dto);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTermReport(Guid id)
        {
            var result = await _termReportService.DeleteTermReportAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-student-term")]
        public async Task<IActionResult> GetByStudentAndTerm([FromQuery] string admissionNo, [FromQuery] Guid termId)
        {
            if (string.IsNullOrWhiteSpace(admissionNo) || termId == Guid.Empty)
                return BadRequest("Invalid student or term info.");

            var result = await _termReportService.GetTermReportByStudentAndTermAsync(admissionNo, termId);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-student/{admissionNo}")]
        public async Task<IActionResult> GetByStudent(string admissionNo)
        {
            var result = await _termReportService.GetTermReportsByStudentAsync(admissionNo);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("by-term/{termId:guid}")]
        public async Task<IActionResult> GetByTerm(Guid termId)
        {
            var result = await _termReportService.GetTermReportsByTermAsync(termId);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("by-class-term")]
        public async Task<IActionResult> GetByClassAndTerm([FromQuery] Guid classId, [FromQuery] Guid termId)
        {
            if (classId == Guid.Empty || termId == Guid.Empty)
                return BadRequest("Invalid class or term ID.");

            var result = await _termReportService.GetTermReportsByClassAsync(classId, termId);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }
    }

}
