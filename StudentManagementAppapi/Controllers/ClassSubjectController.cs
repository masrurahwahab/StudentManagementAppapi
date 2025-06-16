using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;


namespace StudentManagementAppapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClassSubjectController : ControllerBase
    {
        private readonly IClassSubjectService _classSubjectService;

        public ClassSubjectController(IClassSubjectService classSubjectService)
        {
            _classSubjectService = classSubjectService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<ClassSubjectDto>>> CreateClassSubject([FromBody] CreateClassSubjectDto dto)
        {
            if (dto == null)
                return BadRequest(ResponseWrapper<ClassSubjectDto>.Failure("Request body cannot be null"));

            var result = await _classSubjectService.CreateAsync(dto);

            if (result.Successs)
                return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<ClassSubjectDto>>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ResponseWrapper<ClassSubjectDto>.Failure("Invalid ID"));

            var result = await _classSubjectService.GetByIdAsync(id);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<ClassSubjectDto>>>> GetAll()
        {
            var result = await _classSubjectService.GetAllAsync();

            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("by-class/{classId}")]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<ClassSubjectDto>>>> GetByClassId(Guid classId)
        {
            if (classId == Guid.Empty)
                return BadRequest(ResponseWrapper<IEnumerable<ClassSubjectDto>>.Failure("Invalid class ID"));

            var result = await _classSubjectService.GetByClassIdAsync(classId);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-subject/{subjectId}")]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<ClassSubjectDto>>>> GetBySubjectId(Guid subjectId)
        {
            if (subjectId == Guid.Empty)
                return BadRequest(ResponseWrapper<IEnumerable<ClassSubjectDto>>.Failure("Invalid subject ID"));

            var result = await _classSubjectService.GetBySubjectIdAsync(subjectId);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-teacher/{teacherId}")]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<ClassSubjectDto>>>> GetByTeacherId(Guid teacherId)
        {
            if (teacherId == Guid.Empty)
                return BadRequest(ResponseWrapper<IEnumerable<ClassSubjectDto>>.Failure("Invalid teacher ID"));

            var result = await _classSubjectService.GetByTeacherIdAsync(teacherId);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<bool>>> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ResponseWrapper<bool>.Failure("Invalid ID"));

            var result = await _classSubjectService.DeleteAsync(id);

            return result.Successs ? Ok(result) : NotFound(result);
        }
    }

}
