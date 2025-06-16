using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementAppapi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using StudentManagementAppapi.Contract.Services;
    using StudentManagementAppapi.DTO.RequestModel;
    using StudentManagementAppapi.Model.Enum;

    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementDto dto)
        {
            if (dto == null || dto.AuthorId == Guid.Empty || string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Invalid announcement data.");

            var result = await _announcementService.CreateAnnouncementAsync(dto);
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid ID.");

            var result = await _announcementService.GetAnnouncementByIdAsync(id);
            return result.Successs ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var result = await _announcementService.GetAllAnnouncementsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAnnouncements()
        {
            var result = await _announcementService.GetActiveAnnouncementsAsync();
            return result.Successs ? Ok(result) : StatusCode(500, result);
        }

        //[HttpGet("audience/{audience}")]
        //public async Task<IActionResult> GetByTargetAudience(TargetAudience audience)
        //{
        //    var result = await _announcementService..GetAnnouncementsByTargetAudienceAsync(audience);
        //    return result.Successs ? Ok(result) : StatusCode(500, result);
        //}

        //[HttpGet("class/{classId:guid}")]
        //public async Task<IActionResult> GetByClassId(Guid classId)
        //{
        //    if (classId == Guid.Empty)
        //        return BadRequest("Invalid class ID.");

        //    var result = await _announcementService.GetAnnouncementsByClassAsync(classId);
        //    return result.Successs ? Ok(result) : StatusCode(500, result);
        //}

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAnnouncement(Guid id, [FromBody] CreateAnnouncementDto dto)
        {
            if (id == Guid.Empty || dto == null)
                return BadRequest("Invalid input data.");

            var result = await _announcementService.UpdateAnnouncementAsync(id, dto);
            if (!result.Successs)
            {
                return result.Message == "Announcement not found"
                    ? NotFound(result)
                    : StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid announcement ID.");

            var result = await _announcementService.DeleteAnnouncementAsync(id);
            if (!result.Successs)
            {
                return result.Message == "Announcement not found"
                    ? NotFound(result)
                    : StatusCode(500, result);
            }

            return Ok(result);
        }
    }

}
