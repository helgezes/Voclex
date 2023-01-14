using Microsoft.Extensions.DependencyInjection;

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

        private static IServiceCollection AddDefaultHttpClient(this IServiceCollection collection)
        {
            return collection.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:61072/") });
        }

        private static void AddSuggestionsHttpClient(this IServiceCollection collection)
        {
            collection.AddHttpClient(SuggestionsHttpClientName,
                client => client.BaseAddress = new Uri("http://localhost:55659/"));
        }
    }
}
