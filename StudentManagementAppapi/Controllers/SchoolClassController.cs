using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolClassService;

        public SchoolClassController(ISchoolClassService schoolClassService)
        {
            _schoolClassService = schoolClassService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] SchoolClassRequestModel model)
        {
            var response = _schoolClassService.Add(model);
            return StatusCode(response.IsSuccessful ? 201 : 400, response);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var response = _schoolClassService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var response = _schoolClassService.GetById(id);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var response = _schoolClassService.GetByName(name);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpPut("update/{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] SchoolClassRequestModel model)
        {
            var response = _schoolClassService.Update(id, model);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }

        [HttpDelete("delete/{name}")]
        public IActionResult Delete(string name)
        {
            var response = _schoolClassService.Delete(name);
            return StatusCode(response.IsSuccessful ? 200 : 404, response);
        }
    }

}
