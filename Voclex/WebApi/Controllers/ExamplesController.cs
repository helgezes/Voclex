using Application.Models;
using Application.Services;
using Application.Services.Interfaces;
using SharedLibrary.DataTransferObjects;

namespace WebApi.Controllers
{
	public class ExamplesController : GenericTermsRelatedController<Example, ExampleDto>
	{
        public ExamplesController(TermRelatedService<Example, ExampleDto> service,
	        ICrudService<Example, ExampleDto> crudService) : base(service, crudService)
        {
        }
    }
}
