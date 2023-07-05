using System.Security.Authentication;
using Application.DataAccess;
using Application.Models;
using Application.Queries.TermsDictionary;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Services.Interfaces;

namespace WebApi.Controllers
{
	[Route("[controller]")]
	public class TermsDictionaryController : GenericCrudController<TermsDictionary, TermsDictionaryDto>
    {
        private readonly IGetListService<TermsDictionary, TermsDictionaryDto> listService;

        public TermsDictionaryController(
	        IGetListService<TermsDictionary, TermsDictionaryDto> listService,
            ICrudService<TermsDictionary, TermsDictionaryDto> crudService) : base(crudService)
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

        [HttpGet(nameof(GetFirstDictionaryId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<int?> GetFirstDictionaryId([FromServices] IDbContext context, [FromServices] IAuthenticatedUserService userService)
        {
            var user = await userService.GetCurrentUser();

            if (user == null)
                throw new AuthenticationException();

			var dictionaryId = context.TermsDictionaries
                .Where(d => d.UserId == user.Id)
                .OrderBy(d => d.Id)
                .Select(d => d.Id).FirstOrDefault();

            return dictionaryId != 0 ? dictionaryId : null;
		}
    }
}
