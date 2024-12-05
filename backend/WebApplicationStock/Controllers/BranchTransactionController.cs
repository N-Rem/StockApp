using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchTransactionController : ControllerBase
    {
        private readonly IBranchTransactionService _branchTransactionService;

        public BranchTransactionController(IBranchTransactionService branchTransactionService)
        {
            _branchTransactionService = branchTransactionService;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _branchTransactionService.GetAllAsync();
                return Ok(list);
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

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var obj = await _branchTransactionService.GetByIdAsync(id);
                return Ok(obj);
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
        public async Task<IActionResult> Create([FromBody] BranchTransactionCreateRequestDTO request)
        {
            try
            {
                var obj = await _branchTransactionService.CreateAsync(request);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("[Action]/{id}")]
        public async Task<IActionResult> Update([FromBody] BranchTransactionUpdateRequestDTO request, [FromRoute] int id)
        {
            try
            {
                await _branchTransactionService.UpdateAsync(request, id);
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
                await _branchTransactionService.DeleteAsync(id);
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
