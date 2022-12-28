using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public sealed class PicturesService
    {
        private readonly GenericCrudService<Picture, IPictureDto> crudService;
        private readonly IFileSavingService fileSavingService;

        public PicturesService(
            GenericCrudService<Picture, IPictureDto> crudService, 
            IFileSavingServiceFactory fileSavingServiceFactory)
        {
            this.crudService = crudService;
            this.fileSavingService = fileSavingServiceFactory.GetPictureFileSavingService();
        }

        public async Task<Picture> Create(IPictureDto pictureDto, IFormFile file)
        {
            var filePath = await fileSavingService.SaveFileAsync(file);
            pictureDto.Path = filePath;
            return await crudService.Create(pictureDto);
        }
    }
}
