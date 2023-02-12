using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public interface IFileSavingService
{
    Task<string> SaveFileAsync(IFormFile file);

    void DeleteFile(string fileName);
}