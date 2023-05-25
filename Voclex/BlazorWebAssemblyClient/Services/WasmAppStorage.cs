using Microsoft.JSInterop;
using RazorLibrary.Services.Interfaces;

namespace BlazorWebAssemblyClient.Services;

public sealed class WasmAppStorage : IAppStorage
{
    private readonly IJSRuntime jsr;

    public WasmAppStorage(IJSRuntime jsr)
    {
        this.jsr = jsr;
    }

    public async Task<string?> GetItem(string key, CancellationToken cancellationToken)
    {
        return await jsr.InvokeAsync<string?>("localStorage.getItem", cancellationToken, key);
    }

    public async Task SetItem(string key, string value)
    {
        await jsr.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task RemoveItem(string key)
    {
        await jsr.InvokeVoidAsync("localStorage.removeItem", key);
    }
}