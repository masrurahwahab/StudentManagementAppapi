using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaryRecordsController : ControllerBase
    {
        private readonly IDisciplinaryService _disciplinaryService;

        public DisciplinaryRecordsController(IDisciplinaryService disciplinaryService)
        {
            _disciplinaryService = disciplinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDisciplinaryDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid request.");

            var result = await _disciplinaryService.CreateDisciplinaryRecordAsync(dto);
            return result.Successs ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _disciplinaryService.GetDisciplinaryRecordByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _disciplinaryService.GetAllDisciplinaryRecordsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateDisciplinaryDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var result = await _disciplinaryService.UpdateDisciplinaryRecordAsync(id, dto);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _disciplinaryService.DeleteDisciplinaryRecordAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-student/{studentId:guid}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var result = await _disciplinaryService.GetDisciplinaryRecordsByStudentAsync(studentId);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatus([FromQuery] DisciplinaryStatus status)
        {
            var result = await _disciplinaryService.GetDisciplinaryRecordsByStatusAsync(status);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] DisciplinaryStatus status)
        {
            var result = await _disciplinaryService.UpdateDisciplinaryStatusAsync(id, status);
            return result.Successs ? Ok(result) : NotFound(result);
        }
    }

}
