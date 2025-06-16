using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;


namespace StudentManagementAppapi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceDto dto)
        {
            if (dto == null || dto.StudentId == Guid.Empty)
            {
                return BadRequest("Invalid attendance data.");
            }

            var result = await _attendanceService.MarkAttendanceAsync(dto);
            if (!result.Successs)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet("student/{studentId:guid}")]
        public async Task<IActionResult> GetAttendanceByStudentId(Guid studentId)
        {
            if (studentId == Guid.Empty)
            {
                return BadRequest("Invalid student ID.");
            }

            var result = await _attendanceService.GetAttendanceByStudentIdAsync(studentId);
            if (!result.Successs)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet("date/{date:datetime}")]
        public async Task<IActionResult> GetAttendanceByDate(DateTime date)
        {
            var result = await _attendanceService.GetAttendanceByDateAsync(date);
            if (!result.Successs)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpGet("class/{classId:guid}/date/{date:datetime}")]
        public async Task<IActionResult> GetAttendanceByClassAndDate(Guid classId, DateTime date)
        {
            if (classId == Guid.Empty)
            {
                return BadRequest("Invalid class ID.");
            }

            var result = await _attendanceService.GetAttendanceByClassAndDateAsync(classId, date);
            if (!result.Successs)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAttendance(Guid id, [FromBody] MarkAttendanceDto dto)
        {
            if (id == Guid.Empty || dto == null)
            {
                return BadRequest("Invalid request.");
            }

            var result = await _attendanceService.UpdateAttendanceAsync(id, dto);
            if (!result.Successs)
            {
                if (result.Message == "Attendance record not found")
                    return NotFound(result);

                return StatusCode(500, result);
            }

            return Ok(result);
        }
    }

}
