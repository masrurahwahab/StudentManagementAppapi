using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{ 

    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentsController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessmentDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid assessment data.");

            var result = await _assessmentService.CreateAssessmentAsync(dto);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAssessmentById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid assessment ID.");

            var result = await _assessmentService.GetAssessmentByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssessments()
        {
            var result = await _assessmentService.GetAllAssessmentsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAssessment(Guid id, [FromBody] CreateAssessmentDto dto)
        {
            if (id == Guid.Empty || dto == null)
                return BadRequest("Invalid input.");

            var result = await _assessmentService.UpdateAssessmentAsync(id, dto);
            if (!result.Successs)
            {
                return result.Message == "Assessment not found"
                    ? NotFound(result)
                    : StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAssessment(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid assessment ID.");

            var result = await _assessmentService.DeleteAssessmentAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet("class/{classId:guid}")]
        public async Task<IActionResult> GetAssessmentsByClass(Guid classId)
        {
            if (classId == Guid.Empty)
                return BadRequest("Invalid class ID.");

            var result = await _assessmentService.GetAssessmentsByClassAsync(classId);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        //[HttpGet("term/{termId:guid}")]
        //public async Task<IActionResult> GetAssessmentsByTerm(Guid termId)
        //{
        //    if (termId == Guid.Empty)
        //        return BadRequest("Invalid term ID.");

        //    var result = await _assessmentService..GetAssessmentsByTermAsync(termId);
        //    return result.Successs ? Ok(result) : StatusCode(500, result);
        //}

        //[HttpGet("teacher/{teacherId:guid}")]
        //public async Task<IActionResult> GetAssessmentsByTeacher(Guid teacherId)
        //{
        //    if (teacherId == Guid.Empty)
        //        return BadRequest("Invalid teacher ID.");

        //    var result = await _assessmentService.GetAssessmentsByTeacherAsync(teacherId);
        //    return result.Successs ? Ok(result) : StatusCode(500, result);
        //}
    }

}
