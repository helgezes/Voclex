using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces;

public interface IFileSavingService
{
    Task<string> SaveFileAsync(IFormFile file);

    Task<string> SaveFileAsync(Stream fileStream, string fileName);

    void DeleteFile(string fileName);
}