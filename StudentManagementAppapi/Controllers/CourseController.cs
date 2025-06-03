using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Model.Entities;
using System.Xml.Linq;

namespace StudentManagementAppapi.Controllers
{
   [ApiController]
        [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("add")]
        public IActionResult AddCourse([FromBody] CreateCourseRequestModel model)
        {
            var response = _courseService.AddNewCourse(model);
            if (response == null)
                return StatusCode(500, "Internal server error.");

            return StatusCode(response.IsSuccessful ? 201 : 400, response);
        }

        [HttpGet("all")]
        public IActionResult GetAllCourses()
        {
            var response = _courseService.GetAllCourse();
            if (response == null)
                return StatusCode(500, "Internal server error.");

            return Ok(response);
        }

        [HttpGet("{coursename}")]
        public IActionResult GetCourseByName(string coursename)
        {
            var response = _courseService.GetCourseByName(coursename);
            if (response == null)
                return StatusCode(500, "Internal server error.");

            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpDelete("{coursename}")]
        public IActionResult DeleteCourse(string coursename)
        {
            var response = _courseService.DeleteCourse(coursename);
            if (response == null)
                return StatusCode(500, "Internal server error.");

            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        
        [HttpPut("update")]
        public IActionResult UpdateCourse([FromBody] CreateCourseRequestModel model)
        {
            var response = _courseService.UpdateCourse(model);
            if(response == null)
                return StatusCode(500, "Internal server error.");
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }
    }

}
