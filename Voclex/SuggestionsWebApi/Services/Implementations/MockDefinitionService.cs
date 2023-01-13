using SuggestionsWebApi.Services.Implementations.Interfaces;

namespace SuggestionsWebApi.Services.Implementations;

public sealed class MockDefinitionService : IDefinitionSuggestionsService
{
	public string Id => "mock";

	private readonly Dictionary<string, string[]> termsToDefinitions = new() 
	{
		{
			"train", new[] { "A series of connected railroad cars pulled or pushed by one or more locomotives.",
				"A long line of moving people, animals, or vehicles.",
				"The personnel, vehicles, and equipment following and providing supplies and services to a combat unit." }
		},
		{
			"benevolent", new[] { "Characterized by or given to doing good.",
				"Suggestive of doing good; agreeable.",
				"Relating to a charitable organization that operates without making a profit." }
		}
	};
	
	public string[] GetList(string term)
	{
		if(termsToDefinitions.ContainsKey(term))
			return termsToDefinitions[term];

		return Array.Empty<string>();
	}
}