using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.Queries.TermsDictionary;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	public class TermsDictionaryController : ControllerBase
    {
        private readonly GenericGetListService<TermsDictionary, TermsDictionaryDto> listService;

        public TermsDictionaryController(GenericGetListService<TermsDictionary, TermsDictionaryDto> listService)
        {
            this.listService = listService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermsDictionaryDto>> GetList([FromQuery] TermsDictionariesListQuery query) 
        {
            return listService.GetAsync(query);
        }

        [HttpGet(nameof(GetCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<int> GetCount([FromQuery] TermsDictionariesQuery query)
        {
            return listService.GetCount(query);
        }
    }
}
