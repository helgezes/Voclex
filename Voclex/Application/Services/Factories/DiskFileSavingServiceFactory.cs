using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;

namespace Application.Services.Factories;

public sealed class DiskFileSavingServiceFactory : IFileSavingServiceFactory
{
    private readonly string[] picturesExtensions = { ".gif", ".jpeg", ".jpg", ".png" };
    private const string PicturesFolderPath = @"C:\ProgramData\Voclex\Pictures\";
    public IFileSavingService GetPictureFileSavingService()
    {
        return new DiskFileSavingService(picturesExtensions, PicturesFolderPath);
    }
}