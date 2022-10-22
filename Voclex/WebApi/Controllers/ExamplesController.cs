using Application.Models;
using Application.Services;
using Shared.DataTransferObjects;

namespace WebApi.Controllers
{
	public class ExamplesController : GenericTermsRelatedController<Example, ExampleDto>
	{
        public ExamplesController(TermRelatedService<Example, ExampleDto> service) : base(service)
        {
        }
    }
}
