using RazorLibrary.Extensions;
using RazorLibrary.Services.Interfaces;
using System.Net.Http;
using RazorLibrary.Helpers;

namespace MauiApplication.Services
{
    public sealed class MauiAppHttpClient : IAppHttpClient
    {
        private readonly Lazy<HttpClient> _apiClient;
        private readonly Lazy<HttpClient> _suggestionsClient;

        public HttpClient ApiClient => _apiClient.Value;
        public HttpClient SuggestionsClient => _suggestionsClient.Value;

        public MauiAppHttpClient(IServiceProvider provider)
        {
            _apiClient = new Lazy<HttpClient>(() =>
            {
                var storage = provider.GetRequiredService<IAppStorage>();
                var authorizationHeaderHandler = new AuthorizationHeaderHandler(storage)
                {
                    InnerHandler = new HttpClientHandler()
                };
                return new HttpClient(authorizationHeaderHandler)
                {
                    BaseAddress = new Uri("http://ip:5279/")
                };
            });

            _suggestionsClient = new Lazy<HttpClient>(() =>
                new HttpClient(new HttpClientHandler())
                {
                    BaseAddress = new Uri("http://ip:5285/")
                });
        }
    }

}
