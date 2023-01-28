using Microsoft.AspNetCore.Mvc;
using SuggestionsWebApi.Services;
using SuggestionsWebApi.Services.Interfaces;

namespace SuggestionsWebApi.Controllers
{
	[Route("[controller]")]
	public sealed class DefinitionsController : ControllerBase
	{
		private readonly IDefinitionsService definitionsService;

		public DefinitionsController(IDefinitionsService definitionsService)
		{
			this.definitionsService = definitionsService;
		}

		[HttpGet]
		public string[] GetList([FromQuery] string term, [FromQuery] string[]? serviceIds)
		{
			return definitionsService.GetList(term, serviceIds ?? Array.Empty<string>());
		}
	}
}
