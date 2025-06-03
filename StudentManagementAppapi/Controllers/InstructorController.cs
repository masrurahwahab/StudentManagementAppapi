using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost("add")]
        public IActionResult AddInstructor([FromBody] SiginRequestModel model)
        {
            var response = _instructorService.AddNewInstructor(model);
            return StatusCode(response.IsSuccessful ? 201 : 400, response);
        }

        [HttpGet("all")]
        public IActionResult GetAllInstructors()
        {
            var response = _instructorService.GetAllInstructor();
            return Ok(response);
        }

        [HttpGet("{fullname}")]
        public IActionResult GetInstructorByName(string fullname)
        {
            var response = _instructorService.GetInstructorByName(fullname);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpDelete("{fullname}")]
        public IActionResult DeleteInstructor(string fullname)
        {
            var response = _instructorService.RemoveInstructor(fullname);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpPut("update")]
        public IActionResult UpdateInstructor([FromBody] UpdateInstructorProfile profile)
        {
            var response = _instructorService.UpdateInstructorProfile(profile);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }
    }

}
