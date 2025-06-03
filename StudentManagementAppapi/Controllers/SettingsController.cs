using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Settings.Interfaces;

namespace StudentManagementAppapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost("reset-password/{userId:guid}")]
        public IActionResult ResetPassword(Guid userId, [FromBody] ForgetPassword model)
        {
            var response = _settingService.ResetPassword(userId, model);
            return StatusCode(response.IsSuccessful ? 200 : 400, response);
        }
    }

}
