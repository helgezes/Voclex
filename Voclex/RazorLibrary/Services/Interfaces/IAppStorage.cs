namespace RazorLibrary.Services.Interfaces
{
    public interface IAppStorage
    {
        Task<string?> GetItem(string key, CancellationToken cancellationToken);
        Task SetItem(string key, string value);
        Task RemoveItem(string key);
    }
}
