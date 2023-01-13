namespace SuggestionsWebApi.Services.Interfaces;

public interface IDefinitionsService
{
	string[] GetList(string term, string[] serviceIds);
}