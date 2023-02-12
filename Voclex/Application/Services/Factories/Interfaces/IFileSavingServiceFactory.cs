using Application.Services.Interfaces;

namespace Application.Services.Factories.Interfaces;

public interface IFileSavingServiceFactory
{
    IFileSavingService GetPictureFileSavingService();
}