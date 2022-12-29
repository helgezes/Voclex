using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;

namespace Application.Services.Factories;

public sealed class DiskFileSavingServiceFactory : IFileSavingServiceFactory
{
    public const string PicturesFolderPath = @"C:\ProgramData\Voclex\Pictures\";
    public const string PicturesEndpointFolderPath = @"/Storage/Pictures/";

    private readonly string[] picturesExtensions = { ".gif", ".jpeg", ".jpg", ".png" };

    public IFileSavingService GetPictureFileSavingService()
    {
        return new DiskFileSavingService(picturesExtensions, PicturesFolderPath, PicturesEndpointFolderPath);
    }
}