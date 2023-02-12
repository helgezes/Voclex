using Microsoft.JSInterop;

namespace RazorLibrary.Helpers
{
    public sealed class LocalStorage
    {
        private readonly IJSRuntime jsr;

        public LocalStorage(IJSRuntime jsr)
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
    }
}
