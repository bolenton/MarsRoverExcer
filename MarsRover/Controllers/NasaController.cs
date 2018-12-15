using System;
using System.Linq;
using System.Threading.Tasks;
using MarsRover.Service;
using MarsRover.Service.Error;
using Microsoft.AspNetCore.Mvc;

namespace MarsRover.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class NasaController : ControllerBase
    {
        private readonly INasaService _nasaService;
        private readonly ILogger _logger;

        public NasaController(INasaService nasaService, ILogger logger)
        {
            _logger = logger;
            _nasaService = nasaService;
        }

        [HttpGet("photos")]
        public async Task<IActionResult> Get(string rover, DateTime earthDate, int maxPhoto = 25)
        {
            try
            {
                var result = await _nasaService.GetPhoto(rover, earthDate);

                if(!result.Any())
                    return NotFound(new {message = "No no no images were found for the specified date."});

                result = result.OrderByDescending(p => p.EarthDate).Take(maxPhoto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.WriteErrorLog(ex.Message);
                return StatusCode(500, new { error = ex.Message});
            }
            
        }

        [HttpGet("rovers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _nasaService.GetAllRovers();

                return result.Any()
                    ? Ok(result) as IActionResult
                    : NotFound(new {message = "Looks like NASA departed abandoned Mars."});
            }
            catch (Exception ex)
            {
                _logger.WriteErrorLog(ex.Message);
                return StatusCode(500, new { error = ex.Message});
            }
        }

        [HttpGet("defaultDate")]
        public async Task<IActionResult> GetDefaultData(string rover = "curiosity")
        {
            try
            {
                var result = await _nasaService.GetDefaultDate(rover);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.WriteErrorLog(ex.Message);
                return StatusCode(500, new { error = "Internal Server Error, Please try again later" });
            }
            
        }
    }
}
