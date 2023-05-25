using MauiApplication.Services;
using RazorLibrary.Extensions;
using RazorLibrary.Services.Interfaces;

namespace MauiApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<ISecureStorage>(_ => SecureStorage.Default);
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddSingleton<IAppStorage, MauiAppStorage>();

            builder.Services.AddVoclexClientServices();
            builder.Services.AddSingleton<IAppHttpClient, MauiAppHttpClient>(); 

            return builder.Build();
        }
    }
}