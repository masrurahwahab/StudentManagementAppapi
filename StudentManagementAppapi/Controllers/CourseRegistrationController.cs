using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Services;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseRegistrationController : ControllerBase
    {
        private readonly ICourseRegistrationService _courseRegistrationService;

        public CourseRegistrationController(ICourseRegistrationService courseRegistrationService)
        {
            _courseRegistrationService = courseRegistrationService;
        }

        
        [HttpPost("enroll")]
        public IActionResult EnrollForCourse([FromBody] Enrollforacourse model)
        {
            var response = _courseRegistrationService.EnrollForACourse(model);
            if (response.IsSuccessful)
            {
                return CreatedAtAction(nameof(GetEnrollCourse), new { fullname = model.FullName }, response);
            }
            return StatusCode(response.IsSuccessful ? 200 : 400, response);
        }

       
        [HttpGet("all")]
        public IActionResult GetAllEnrollCourses()
        {
            var response = _courseRegistrationService.GetAllEnrollCourses();            
            if (response == null)
                return StatusCode(500, "Internal server error.");

            return Ok(response);
        }

        
        [HttpGet("{fullname}")]
        public IActionResult GetEnrollCourse(string fullname)
        {
            var response = _courseRegistrationService.GetEnrollCourse(fullname);
            if (response.IsSuccessful)
                return Ok(response);
            return NotFound(response);
        }
    }

}
