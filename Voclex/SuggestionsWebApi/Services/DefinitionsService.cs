using SuggestionsWebApi.Services.Implementations.Interfaces;
using SuggestionsWebApi.Services.Interfaces;

namespace SuggestionsWebApi.Services
{
	public sealed class DefinitionsService : IDefinitionsService
	{
		private readonly IEnumerable<IDefinitionSuggestionsService> suggestionsServices;

		public DefinitionsService(IEnumerable<IDefinitionSuggestionsService> suggestionsServices)
		{
			this.suggestionsServices = suggestionsServices;
		}

		public string[] GetList(string term, string[] serviceIds)
		{
			return suggestionsServices
				.Where(s => !serviceIds.Any() || serviceIds.Contains(s.Id))
				.SelectMany(s => s.GetList(term)).ToArray();
		}
	}
}
