using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using RazorLibrary.Helpers;
using RazorLibrary.Services;
using SharedLibrary.Services;
using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Extensions
{
    public static class ConfigureServices
    {
        public const string SuggestionsHttpClientName = "Suggestions";

        public static IServiceCollection AddVoclexClientServices(this IServiceCollection collection)
        {
            collection.AddDefaultHttpClient();
            collection.AddSuggestionsHttpClient();

            collection.AddScoped<LocalStorage>();
            collection.AddSingleton<ITokenValidator, ClientJwtTokenValidator>();
            collection.AddScoped<IUserStorage, BrowserUserStorage>();
            collection.AddScoped<IAuthenticatedUserService, JwtAuthenticatedUserService>();

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
        private readonly LocalStorage jsr;

        public AuthorizationHeaderHandler(LocalStorage jsr)
        {
            this.jsr = jsr;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await jsr.GetItem("token", cancellationToken);

            if (token != null)
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
