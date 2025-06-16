using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<Classresponse>>> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            if (createClassDto == null)
                return BadRequest(ResponseWrapper<Classresponse>.Failure("Request body cannot be null"));

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(ResponseWrapper<Classresponse>.Failure($"Validation failed: {errors}"));
            }

            var result = await _classService.CreateClassAsync(createClassDto);

            if (result.Successs)
                return CreatedAtAction(nameof(GetClassById), new { id = result.Data.Id }, result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<Classresponse>>> GetClassById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ResponseWrapper<Classresponse>.Failure("Invalid class ID provided"));

            var result = await _classService.GetClassByIdAsync(id);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<Classresponse>>>> GetAllClasses()
        {
            var result = await _classService.GetAllClassesAsync();

            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("academic-year/{academicYear}")]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<Classresponse>>>> GetClassesByAcademicYear(string academicYear)
        {
            if (string.IsNullOrWhiteSpace(academicYear))
                return BadRequest(ResponseWrapper<IEnumerable<Classresponse>>.Failure("Academic year cannot be empty"));

            var result = await _classService.GetClassesByAcademicYearAsync(academicYear);

            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper<Classresponse>>> UpdateClass(Guid id, [FromBody] CreateClassDto updateDto)
        {
            if (id == Guid.Empty)
                return BadRequest(ResponseWrapper<Classresponse>.Failure("Invalid class ID provided"));

            if (updateDto == null)
                return BadRequest(ResponseWrapper<Classresponse>.Failure("Request body cannot be null"));

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(ResponseWrapper<Classresponse>.Failure($"Validation failed: {errors}"));
            }

            var result = await _classService.UpdateClassAsync(id, updateDto);

            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<bool>>> DeleteClass(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ResponseWrapper<bool>.Failure("Invalid class ID provided"));

            var result = await _classService.DeleteClassAsync(id);

            return result.Successs ? Ok(result) : NotFound(result);
        }
    }

}