using RazorLibrary.Helpers;
using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Services.Authentication
{
    public sealed class BrowserUserStorage : IUserStorage
    {
        private readonly LocalStorage storage;

        public BrowserUserStorage(LocalStorage storage)
        {
            this.storage = storage;
        }

        public async Task<string?> GetCurrentUserToken()
        {
            return await storage.GetItem("token", CancellationToken.None);
        }
    }
}
