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

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Notification data is required.");
            }

            var response = await _notificationService.CreateNotificationAsync(dto);
            if (!response.Successs)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetNotificationById), new { id = response.Data.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid notification ID.");
            }

            var response = await _notificationService.GetNotificationByIdAsync(id);
            if (!response.Successs)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var response = await _notificationService.GetAllNotificationsAsync();
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }

            var response = await _notificationService.GetNotificationsByUserIdAsync(userId);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("user/{userId:guid}/unread")]
        public async Task<IActionResult> GetUnreadNotificationsByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }

            var response = await _notificationService.GetUnreadNotificationsByUserIdAsync(userId);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPatch("{id:guid}/mark-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid notification ID.");
            }

            var response = await _notificationService.MarkNotificationAsReadAsync(id);
            if (!response.Successs)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPatch("user/{userId:guid}/mark-all-as-read")]
        public async Task<IActionResult> MarkAllNotificationsAsRead(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID.");
            }

            var response = await _notificationService.MarkAllNotificationsAsReadAsync(userId);
            if (!response.Successs)
            {
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid notification ID.");
            }

            var response = await _notificationService.DeleteNotificationAsync(id);
            if (!response.Successs)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }

}
