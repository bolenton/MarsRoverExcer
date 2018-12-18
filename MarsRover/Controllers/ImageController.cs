using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsRover.Service.Error;
using MarsRover.Service.Image;
using Microsoft.AspNetCore.Mvc;

namespace MarsRover.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IImageService _imageService;

        public ImageController(ILogger logger, IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadAsync(Uri requestUri, string filename)
        {
            try
            {
                if (!filename.ToLower().EndsWith("jpg") || !requestUri.OriginalString.ToLower().EndsWith("jpg"))
                    return BadRequest(new
                        {errorMessage = "This is not a valid image, only file type of JPG is supported."});

                await _imageService.SaveJpegImageAsync(requestUri, filename);
                var image = _imageService.RetrieveJpgImage(filename);

                return File(image, "image/jpeg");
            }
            catch (Exception ex)
            {
                _logger.WriteErrorLog(ex.Message);
                return StatusCode(500, new { error = "Something bad happen, but don't worry our team is on it." });
            }
        }

        [HttpGet("downloadlist")]
        public IActionResult GetPreviouslyDownloadAsync()
        {
            try
            {
                var images = _imageService.RetrieveAllJpgImage();

                if (!images.Any())
                    NoContent();

                return Ok(images);
            }
            catch (Exception ex)
            {
                _logger.WriteErrorLog(ex.Message);
                return StatusCode(500, new { error = "Something bad happen, but don't worry our team is on it." });
            }
        }
    }
}