using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.Queries.Terms;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class TermsController : GenericCrudController<Term, TermDto>
    {
        private readonly IGetListService<Term, TermDto> listService;
        public TermsController(ICrudService<Term, TermDto> crudService,
	        IGetListService<Term, TermDto> listService) : base(crudService)
        {
            this.listService = listService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<TermDto>> GetList([FromQuery] TermsListQuery query) //todo check validity of page and pagesize
        {
            return listService.GetAsync(query);
        }

        [HttpGet(nameof(GetCount))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<int> GetCount([FromQuery] TermsQuery query)
        {
            return listService.GetCount(query);
        }

    }
}
