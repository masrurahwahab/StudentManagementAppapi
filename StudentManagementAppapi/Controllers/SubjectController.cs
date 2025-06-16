using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid subject data.");

            var result = await _subjectService.CreateSubjectAsync(dto);
            return result.Successs ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid subject ID.");

            var result = await _subjectService.GetSubjectByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var result = await _subjectService.GetAllSubjectsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] CreateSubjectDto dto)
        {
            if (id == Guid.Empty || dto == null)
                return BadRequest("Invalid input.");

            var result = await _subjectService.UpdateSubjectAsync(id, dto);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid subject ID.");

            var result = await _subjectService.DeleteSubjectAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("class-level/{classLevel}")]
        public async Task<IActionResult> GetSubjectsByClassLevel(ClassLevel classLevel)
        {
            var result = await _subjectService.GetSubjectsByClassLevelAsync(classLevel);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }
    }

}
