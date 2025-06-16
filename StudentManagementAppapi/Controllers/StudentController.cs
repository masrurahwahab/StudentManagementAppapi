using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;


namespace StudentManagementAppapi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudent dto)
        {
            if (dto == null || dto.User == null || dto.ClassId == Guid.Empty)
                return BadRequest("Invalid student data.");

            var result = await _studentService.CreateStudentAsync(dto);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid student ID.");

            var result = await _studentService.GetStudentByIdAsync(id);
            if (!result.Successs)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("admission/{admissionNumber}")]
        public async Task<IActionResult> GetByAdmissionNumber(string admissionNumber)
        {
            if (string.IsNullOrWhiteSpace(admissionNumber))
                return BadRequest("Admission number is required.");

            var result = await _studentService.GetByAdmissionNumberAsync(admissionNumber);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudentsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedStudents([FromQuery] int pageNumber = 1, [FromQuery] 
                                                        int pageSize = 10, [FromQuery] string? search = null)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and size must be greater than 0.");

            var result = await _studentService.GetPagedStudentsAsync(pageNumber, pageSize, search);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("class/{classId:guid}")]
        public async Task<IActionResult> GetStudentsByClass(Guid classId)
        {
            if (classId == Guid.Empty)
                return BadRequest("Invalid class ID.");

            var result = await _studentService.GetStudentsByClassAsync(classId);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] CreateStudent dto)
        {
            if (id == Guid.Empty || dto == null || dto.User == null)
                return BadRequest("Invalid update data.");

            var result = await _studentService.UpdateStudentAsync(id, dto);
            if (!result.Successs)
            {
                if (result.Message == "Student not found")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid student ID.");

            var result = await _studentService.DeleteStudentAsync(id);
            if (!result.Successs)
            {
                if (result.Message == "Student not found")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }

}
