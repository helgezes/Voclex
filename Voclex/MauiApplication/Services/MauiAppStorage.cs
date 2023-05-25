using RazorLibrary.Services.Interfaces;

namespace MauiApplication.Services
{
    public sealed class MauiAppStorage : IAppStorage
    {
        private readonly ISecureStorage secureStorage;

        public MauiAppStorage(ISecureStorage secureStorage)
        {
            this.secureStorage = secureStorage;
        }

        public async Task<string> GetItem(string key, CancellationToken cancellationToken)
        {
            return await secureStorage.GetAsync(key);
        }

        public async Task SetItem(string key, string value)
        {
            await secureStorage.SetAsync(key, value);
        }

        public Task RemoveItem(string key)
        {
            secureStorage.Remove(key);
            return Task.CompletedTask;
        }
    }
}
