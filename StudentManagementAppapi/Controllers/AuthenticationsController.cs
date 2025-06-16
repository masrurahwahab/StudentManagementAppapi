using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _authService.LoginAsync(model.Email, model.Password);

                if (!result.Successs)
                    return Unauthorized(result.Message ?? "Invalid email or password.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred during login.");
            }
        }
    }

}
