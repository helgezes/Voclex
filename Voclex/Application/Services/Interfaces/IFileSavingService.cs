using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces;

public interface IFileSavingService
{
    Task<string> SaveFileAsync(IFormFile file);
}