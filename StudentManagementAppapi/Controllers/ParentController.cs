using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementAppapi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using StudentManagementAppapi.Contract.Services;
    using StudentManagementAppapi.DTO.RequestModel;
    using StudentManagementAppapi.DTO.ResponseModel;
    using StudentManagementAppapi.DTO.Wrapper;

    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> CreateParent([FromBody] CreateParentDto createParentDto)
        {
            try
            {
                if (createParentDto == null)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Request body cannot be null"));
                }

                if (!ModelState.IsValid)
                {
                    var errors = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return BadRequest(ResponseWrapper<ParentDto>.Failure($"Validation failed: {errors}"));
                }

                
                if (createParentDto.StudentId == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Valid Student ID is required"));
                }

                if (string.IsNullOrWhiteSpace(createParentDto.Username))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Username is required"));
                }

                if (string.IsNullOrWhiteSpace(createParentDto.Password))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Password is required"));
                }

                if (string.IsNullOrWhiteSpace(createParentDto.Email))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Email is required"));
                }

                var result = await _parentService.CreateParentAsync(createParentDto);

                if (result.Successs)
                {
                    return CreatedAtAction(nameof(GetParentById), new { id = result.Data.Id }, result);
                }

                return BadRequest(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid operation: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while creating the parent"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> GetParentById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Invalid parent ID provided"));
                }

                var result = await _parentService.GetParentByIdAsync(id);

                if (result.Successs)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while retrieving the parent"));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseWrapper<IEnumerable<ParentDto>>>> GetAllParents()
        {
            try
            {
                var result = await _parentService.GetAllParentsAsync();

                if (result.Successs)
                {
                    return Ok(result);
                }

                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<IEnumerable<ParentDto>>.Failure("An unexpected error occurred while retrieving parents"));
            }
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> GetParentByStudentId(Guid studentId)
        {
            try
            {
                if (studentId == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Invalid student ID provided"));
                }

                var result = await _parentService.GetParentByStudentIdAsync(studentId);

                if (result.Successs)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while retrieving the parent"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> UpdateParent(Guid id, [FromBody] CreateParentDto updateDto)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Invalid parent ID provided"));
                }

                if (updateDto == null)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Request body cannot be null"));
                }

                if (!ModelState.IsValid)
                {
                    var errors = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return BadRequest(ResponseWrapper<ParentDto>.Failure($"Validation failed: {errors}"));
                }

               
                if (string.IsNullOrWhiteSpace(updateDto.FirstName))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("First name is required"));
                }

                if (string.IsNullOrWhiteSpace(updateDto.LastName))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Last name is required"));
                }

                if (string.IsNullOrWhiteSpace(updateDto.Email))
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Email is required"));
                }

                var result = await _parentService.UpdateParentAsync(id, updateDto);

                if (result.Successs)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid operation: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while updating the parent"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWrapper<bool>>> DeleteParent(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<bool>.Failure("Invalid parent ID provided"));
                }

                var result = await _parentService.DeleteParentAsync(id);

                if (result.Successs)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<bool>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ResponseWrapper<bool>.Failure($"Invalid operation: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<bool>.Failure("An unexpected error occurred while deleting the parent"));
            }
        }

        [HttpPatch("{id}/activate")]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> ActivateParent(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Invalid parent ID provided"));
                }

                
                var parentResult = await _parentService.GetParentByIdAsync(id);
                if (!parentResult.Successs)
                {
                    return NotFound(parentResult);
                }

               
                return Ok(ResponseWrapper<ParentDto>.Success(parentResult.Data, "Parent activated successfully"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while activating the parent"));
            }
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<ActionResult<ResponseWrapper<ParentDto>>> DeactivateParent(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(ResponseWrapper<ParentDto>.Failure("Invalid parent ID provided"));
                }

                
                var parentResult = await _parentService.GetParentByIdAsync(id);
                if (!parentResult.Successs)
                {
                    return NotFound(parentResult);
                }

               
                return Ok(ResponseWrapper<ParentDto>.Success(parentResult.Data, "Parent deactivated successfully"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ResponseWrapper<ParentDto>.Failure($"Invalid argument: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper<ParentDto>.Failure("An unexpected error occurred while deactivating the parent"));
            }
        }
    }
}
