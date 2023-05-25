using RazorLibrary.Helpers;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.Services.Interfaces;

namespace RazorLibrary.Services.Authentication
{
    public sealed class BrowserUserStorage : IUserStorage
    {
        private readonly IAppStorage storage;

        public BrowserUserStorage(IAppStorage storage)
        {
            this.storage = storage;
        }

        public async Task<string?> GetCurrentUserToken()
        {
            return await storage.GetItem("token", CancellationToken.None);
        }
    }
}
