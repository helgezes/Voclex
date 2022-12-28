using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public sealed class PicturesController : ControllerBase
    {
        private readonly PicturesService picturesService;

        public PicturesController(PicturesService picturesService)
        {
            this.picturesService = picturesService;
        }

        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromForm] PictureDto dto, IFormFile file) 
        {
            if (dto.Id != default(int))
                return BadRequest("This is method for creation. Id should be 0. For updating existing object use different method.");

            try
            {
                var newModel = await picturesService.Create(dto, file);

                return Ok(newModel.Id);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Creation failed. Check your model and try again.");
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
        }
        
    }

}
