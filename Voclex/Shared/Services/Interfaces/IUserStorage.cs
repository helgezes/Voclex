namespace SharedLibrary.Services.Interfaces
{
    public interface IUserStorage
    {
        Task<string?> GetCurrentUserToken();
    }
}
