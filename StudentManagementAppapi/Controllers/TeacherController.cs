using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto dto)
        {
            var result = await _teacherService.CreateTeacherAsync(dto);
            return result.Successs ? Ok(result) : BadRequest(result);
          
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _teacherService.GetTeacherByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _teacherService.GetAllTeachersAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeacherDto dto)
        {
            var result = await _teacherService.UpdateTeacherAsync(id, dto);
            return result.Successs ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-subject")]
        public async Task<IActionResult> GetBySubject([FromQuery] string subject)
        {
            var result = await _teacherService.GetTeachersBySubjectAsync(subject);
            return Ok(result);
        }
    }

}
