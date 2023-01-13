namespace SuggestionsWebApi.Services.Implementations.Interfaces;

public interface IDefinitionSuggestionsService
{
	string Id { get; }
	string[] GetList(string term);
}