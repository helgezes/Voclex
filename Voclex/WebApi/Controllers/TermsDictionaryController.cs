using System.Security.Authentication;
using Application.DataAccess;
using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Queries.TermsDictionary;
using SharedLibrary.Services.Interfaces;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	public class TermsDictionaryController : GenericCrudController<TermsDictionary, TermsDictionaryDto>
    {
	    public TermsDictionaryController(
            ICrudService<TermsDictionary, TermsDictionaryDto> crudService) : base(crudService)
        {
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermsDictionaryDto>> GetList(
	        [FromQuery] TermsDictionariesListQueryDto dto,
	        [FromServices] GenericGetListService<TermsDictionary, TermsDictionariesQueryDto, TermsDictionaryDto> listService) 
        {
            return listService.GetAsync(dto);
        }

        [HttpGet(nameof(GetCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<int> GetCount(
	        [FromQuery] TermsDictionariesQueryDto dto, 
	        [FromServices] GenericGetCountService<TermsDictionary, TermsDictionariesQueryDto> genericGetCountService)
        {
            return genericGetCountService.GetCount(dto);
        }

        [HttpGet(nameof(GetMainDictionaryId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<int> GetMainDictionaryId([FromServices] TermsDictionaryService dictionaryService)
        {
	        return await dictionaryService.GetMainDictionaryId();
		}
    }
}
