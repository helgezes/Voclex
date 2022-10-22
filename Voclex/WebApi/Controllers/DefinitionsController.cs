using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.Queries.Definitions;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class DefinitionsController : ControllerBase
    {
        private readonly TermRelatedService<Definition, DefinitionDto> definitionService;

        public DefinitionsController(TermRelatedService<Definition, DefinitionDto> definitionService)
        {
            this.definitionService = definitionService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<DefinitionDto>> GetList([FromQuery] DefinitionsListQuery definitionsListQuery)
        {
            return definitionService.GetListAsync(definitionsListQuery.TermsIds);
        }
    }
}
