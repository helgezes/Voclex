using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace WebApi.Controllers
{
	public class DefinitionsController : ControllerBase
    {
        private readonly TermRelatedService<Definition, DefinitionDto> definitionService;

        public DefinitionsController(TermRelatedService<Definition, DefinitionDto> definitionService)
        {
            this.definitionService = definitionService;
        }

        [HttpGet(nameof(GetList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IEnumerable<DefinitionDto>> GetList([FromQuery] int[] termsIds)
        {
            return definitionService.GetListAsync(termsIds);
        }
    }
}
