using Application.Models;
using Application.Services;
using SharedLibrary.DataTransferObjects;

namespace WebApi.Controllers
{
	public class ExamplesController : GenericTermsRelatedController<Example, ExampleDto>
	{
        public ExamplesController(TermRelatedService<Example, ExampleDto> service) : base(service)
        {
        }
    }
}
