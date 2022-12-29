using Application.DataAccess;
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
        private readonly IDbContext context;

        public PicturesService(
            GenericCrudService<Picture, IPictureDto> crudService, 
            IFileSavingServiceFactory fileSavingServiceFactory, 
            IDbContext context)
        {
            this.crudService = crudService;
            this.context = context;
            this.fileSavingService = fileSavingServiceFactory.GetPictureFileSavingService();
        }

        public async Task<Picture> Create(IPictureDto pictureDto, IFormFile file, bool saveChanges = true)
        {
            var filePath = await fileSavingService.SaveFileAsync(file);
            pictureDto.Path = filePath;
            return await crudService.Create(pictureDto, saveChanges);
        }

        public async Task<bool> Delete(int id, bool saveChanges = true)
        {
            var picture = await context.Pictures.FindAsync(id);
            if (picture == null) return false;

            fileSavingService.DeleteFile(Path.GetFileName(picture.Path));

            return await crudService.Delete(picture.Id, saveChanges);
        }
    }
}
