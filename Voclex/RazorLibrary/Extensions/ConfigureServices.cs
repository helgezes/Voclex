using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using RazorLibrary.Helpers;
using RazorLibrary.Services.Authentication;
using RazorLibrary.Services.Interfaces;
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

            collection.AddSingleton<ITokenValidator, ClientJwtTokenValidator>();
            collection.AddScoped<IUserStorage, BrowserUserStorage>();
            collection.AddAuthorizationCore();
            collection.AddScoped<ClientJwtAuthenticationStateProvider>();
            collection.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ClientJwtAuthenticationStateProvider>());
            collection.AddScoped<IAuthenticatedUserService, JwtAuthenticatedUserService>();
            collection.AddScoped<LoginService>();

            return collection;
        }

        private static void AddDefaultHttpClient(this IServiceCollection collection)
        {
            collection.AddTransient<AuthorizationHeaderHandler>();
            collection.AddTransient<LogoutOnUnauthorizedHandler>();

            collection.AddHttpClient(Options.DefaultName,
                client =>
                {
                    client.BaseAddress = new Uri("http://localhost:5279/");
                })
                .AddHttpMessageHandler<AuthorizationHeaderHandler>()
                .AddHttpMessageHandler<LogoutOnUnauthorizedHandler>();
        }

        private static void AddSuggestionsHttpClient(this IServiceCollection collection)
        {
            collection.AddHttpClient(SuggestionsHttpClientName,
                (client) => client.BaseAddress = new Uri("http://localhost:5285/"));
        }
    }

    public sealed class AuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly IAppStorage storage;

        public AuthorizationHeaderHandler(IAppStorage storage)
        {
            this.storage = storage;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await storage.GetItem("token", cancellationToken);

            if (token != null)
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public class LogoutOnUnauthorizedHandler : DelegatingHandler
    {
	    private readonly IJSRuntime _jsRuntime;

	    public LogoutOnUnauthorizedHandler(IJSRuntime jsRuntime)
	    {
		    _jsRuntime = jsRuntime;
	    }

	    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	    {
		    var response = await base.SendAsync(request, cancellationToken);

		    if (response.StatusCode == HttpStatusCode.Unauthorized)
		    {
			    await _jsRuntime.InvokeVoidAsync("logOut", cancellationToken);
		    }

		    return response;
	    }
    }

}
