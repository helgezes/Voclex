namespace RazorLibrary.Services.Interfaces
{
    public interface IAppHttpClient
    {
        HttpClient ApiClient { get; }
        HttpClient SuggestionsClient { get; }
    }

}
