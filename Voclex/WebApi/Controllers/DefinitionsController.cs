using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using SharedLibrary.DataTransferObjects;

namespace WebApi.Controllers
{
    public class DefinitionsController : GenericTermsRelatedController<Definition, DefinitionDto>
    {
        public DefinitionsController(TermRelatedService<Definition, DefinitionDto> definitionService,
	        ICrudService<Definition, DefinitionDto> crudService) : 
            base(definitionService, crudService)
        { }
    }
}
