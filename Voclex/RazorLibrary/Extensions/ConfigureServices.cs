using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace RazorLibrary.Extensions
{
    public static class ConfigureServices
    {
        public const string SuggestionsHttpClientName = "Suggestions";

        public static IServiceCollection AddVoclexClientServices(this IServiceCollection collection)
        {
            collection.AddDefaultHttpClient();
            collection.AddSuggestionsHttpClient();

            return collection;
        }

        private static void AddDefaultHttpClient(this IServiceCollection collection)
        {
            collection.AddTransient<AuthorizationHeaderHandler>();

            collection.AddHttpClient(Options.DefaultName,
                client =>
                {
                    client.BaseAddress = new Uri("http://localhost:61072/");
                })
                .AddHttpMessageHandler<AuthorizationHeaderHandler>();
        }

        private static void AddSuggestionsHttpClient(this IServiceCollection collection)
        {
            collection.AddHttpClient(SuggestionsHttpClientName,
                (client) => client.BaseAddress = new Uri("http://localhost:55659/"));
        }
    }

    public sealed class AuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly IJSRuntime jsr;

        public AuthorizationHeaderHandler(IJSRuntime jsr)
        {
            this.jsr = jsr;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await jsr.InvokeAsync<string>("localStorage.getItem", cancellationToken, "token");

            if (token != null)
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
