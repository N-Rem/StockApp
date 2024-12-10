using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalPricesController : ControllerBase
    {
        private readonly IHistoricalPriceService _historicalPriceService;

        public HistoricalPricesController(IHistoricalPriceService historicalPriceService)
        {
            _historicalPriceService = historicalPriceService;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _historicalPriceService.GetAllAsinc();
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
                var obj = await _historicalPriceService.GetByIdAsync(id);
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

        [HttpGet("[Action]/{productId}")]
        public async Task<IActionResult> GetByProduct([FromRoute] int productId)
        {
            try
            {
                var list = await _historicalPriceService.GetByProductAsync(productId);
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

        [HttpDelete("[Action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _historicalPriceService.DeleteAsync(id);
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
