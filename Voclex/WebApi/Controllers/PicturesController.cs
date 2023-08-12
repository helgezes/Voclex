using System.ComponentModel.DataAnnotations;
using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Queries.TermsRelated;
using WebApi.Constants;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public sealed class PicturesController : ControllerBase
    {
        private readonly PicturesService picturesService;
        private readonly ITermRelatedService<PictureDto> listService;
        public PicturesController(PicturesService picturesService, ITermRelatedService<PictureDto> listService)
        {
            this.picturesService = picturesService;
            this.listService = listService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(Duration = CacheConstants.ModulesGetListCacheDurationInSeconds, Location = ResponseCacheLocation.Client)]
        public async Task<IEnumerable<PictureDto>> GetList([FromQuery] TermsRelatedListQuery termsRelatedListQuery)
        {
            return await listService.GetListAsync(termsRelatedListQuery.TermsIds);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromForm] PictureDto dto, IFormFile file)
        {
            return await CreateAndGetOkOrBadRequest(dto, () => picturesService.Create(dto, file));
        }

        [HttpPost("DownloadAndCreate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromForm] PictureDto dto, string pictureUrl)
        {
            return await CreateAndGetOkOrBadRequest(dto, () => picturesService.Create(dto, pictureUrl));
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromForm] PictureDto dto, IFormFile file)
        {
            return this.OkOrNotFound(await picturesService.UpdateAsync(dto, file) != null);
		}


        [HttpPut("DownloadAndEdit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromForm] PictureDto dto, string pictureUrl)
        {
	        return this.OkOrNotFound(await picturesService.UpdateAsync(dto, pictureUrl) != null);
		}


		[HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([Required] int id)
        {
            return this.OkOrNotFound(await picturesService.Delete(id));
        }

        private async Task<ActionResult> CreateAndGetOkOrBadRequest(PictureDto dto, Func<Task<Picture>> createFunc)
        {
            if (dto.Id != default(int))
                return BadRequest(
                    "This is method for creation. Id should be 0. For updating existing object use different method.");

            try
            {
                var newModel = await createFunc();

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
