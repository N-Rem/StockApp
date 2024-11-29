using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); //404 not found
            }
            catch (Exception ex) //Agarra cualquier error inesperado.
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message); //404 not found
            }
            catch (Exception ex) //Agarra cualquier error inesperado.
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateOwner([FromBody] UserCreateRequestDTO request)
        {
            try
            {
                var newUserDTO = await _userService.CreateOwnerAsync(request);
                return Ok(newUserDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateEmployee([FromBody] UserCreateRequestDTO request)
        {
            try
            {
                var newUserDTO = await _userService.CreateEmployeeAsync(request);
                return Ok(newUserDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateSysAdmin([FromBody] UserCreateRequestDTO request)
        {
            try
            {
                var newUserDTO = await _userService.CreateSysAdminAsync(request);
                return Ok(newUserDTO);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequestDTO reques, [FromRoute] int id)
        {
            try
            {
                await _userService.UpdateAsync(reques, id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
