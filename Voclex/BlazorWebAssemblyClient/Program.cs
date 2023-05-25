using BlazorWebAssemblyClient;
using BlazorWebAssemblyClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RazorLibrary.Extensions;
using RazorLibrary.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IAppStorage, WasmAppStorage>();

builder.Services.AddVoclexClientServices(); 

builder.Services.AddSingleton<IAppHttpClient, WasmAppHttpClient>();

await builder.Build().RunAsync();
