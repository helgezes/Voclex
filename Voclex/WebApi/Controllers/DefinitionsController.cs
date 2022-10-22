using Application.Models;
using Application.Services;
using Shared.DataTransferObjects;

namespace WebApi.Controllers
{
    public class DefinitionsController : GenericTermsRelatedController<Definition, DefinitionDto>
    {
        public DefinitionsController(TermRelatedService<Definition, DefinitionDto> definitionService) : 
            base(definitionService)
        { }
    }
}
