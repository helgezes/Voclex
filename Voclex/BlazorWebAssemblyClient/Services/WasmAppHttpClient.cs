using Microsoft.Extensions.Options;
using RazorLibrary.Extensions;
using RazorLibrary.Services.Interfaces;

namespace BlazorWebAssemblyClient.Services;

public class WasmAppHttpClient : IAppHttpClient
{
    public HttpClient ApiClient { get; }
    public HttpClient SuggestionsClient { get; }

    public WasmAppHttpClient(IHttpClientFactory httpClientFactory)
    {
        ApiClient = httpClientFactory.CreateClient(Options.DefaultName);
        SuggestionsClient = httpClientFactory.CreateClient(ConfigureServices.SuggestionsHttpClientName);
    }
}
