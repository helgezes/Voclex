using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Queries.TermsRelated;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public sealed class PicturesController : ControllerBase
    {
        private readonly PicturesService picturesService;
        private readonly TermRelatedService<Picture, PictureDto> listService;
        public PicturesController(PicturesService picturesService, TermRelatedService<Picture, PictureDto> listService)
        {
            this.picturesService = picturesService;
            this.listService = listService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<PictureDto>> GetList([FromQuery] TermsRelatedListQuery termsRelatedListQuery)
        {
            return listService.GetListAsync(termsRelatedListQuery.TermsIds);
        }

        [HttpPost]
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromForm] PictureDto dto, IFormFile file)
        {
            return OkOrNotFound(await picturesService.UpdateAsync(dto, file) != null);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([Required] int id)
        {
            return OkOrNotFound(await picturesService.Delete(id));
        }


        private ActionResult OkOrNotFound(bool condition)
        {
            if (condition)
                return Ok();

            return NotFound();
        }
    }

}
