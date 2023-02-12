using Infrastructure.Services.Factories.Interfaces;

namespace Infrastructure.Services.Factories;

public sealed class DiskFileSavingServiceFactory : IFileSavingServiceFactory
{
    public static readonly string PicturesFolderPath = @$"{Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())}ProgramData/Voclex/Pictures/";
    public const string PicturesEndpointFolderPath = @"/Storage/Pictures/";

    private readonly string[] picturesExtensions = { ".gif", ".jpeg", ".jpg", ".png" };

    public IFileSavingService GetPictureFileSavingService()
    {
        return new DiskFileSavingService(picturesExtensions, PicturesFolderPath, PicturesEndpointFolderPath);
    }
}