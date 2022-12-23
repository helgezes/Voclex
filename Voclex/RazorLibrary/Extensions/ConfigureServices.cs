using Microsoft.Extensions.DependencyInjection;

namespace RazorLibrary.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddVoclexClientServices(this IServiceCollection collection)
        {
            collection.AddHttpClient();

            return collection;
        }

        private static IServiceCollection AddHttpClient(this IServiceCollection collection)
        {
            return collection.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:61072/") });
        }
    }
}
