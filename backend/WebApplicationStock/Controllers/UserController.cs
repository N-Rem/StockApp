using Application.Interfaces;
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
            catch(Exception ex) //Agarra cualquier error inesperado.
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
