using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Queries.Terms;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class TermsController : GenericCrudController<Term, TermDto>
    {
        public TermsController(ICrudService<Term, TermDto> crudService) : base(crudService)
        {
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TermDto>> GetList(
	        [FromQuery] TermsListQueryDto dto,
	        [FromServices] GenericGetListService<Term, TermsQueryDto, TermDto> listService) //todo check validity of page and pagesize
        {
            return await listService.GetAsync(dto);
        }

        [HttpGet(nameof(GetCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<int> GetCount(
	        [FromQuery] TermsQueryDto dto,
	        [FromServices] GenericGetCountService<Term, TermsQueryDto> genericGetCountService)
        {
            return genericGetCountService.GetCount(dto);
        }

    }
}
