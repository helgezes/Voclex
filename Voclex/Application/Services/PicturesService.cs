using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
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
            await SaveFileAndAddPathToDto(pictureDto, file);
            return await crudService.Create(pictureDto, saveChanges);
        }


        public async Task<IPictureDto?> GetByIdAsync(int id)
        {
            return await crudService.GetByIdAsync(id);
        }

        public async Task<Picture?> UpdateAsync(IPictureDto pictureDto, IFormFile file, bool saveChanges = true)
        {
            var modelFromDb = await GetPictureFromContext(pictureDto.Id);
            if (modelFromDb == null) return null;

            DeletePictureFile(modelFromDb);

            await SaveFileAndAddPathToDto(pictureDto, file);

            return await crudService.UpdateAsync(pictureDto, saveChanges);
        }

        public async Task<bool> Delete(int id, bool saveChanges = true)
        {
            var picture = await GetPictureFromContext(id);
            if (picture == null) return false;

            DeletePictureFile(picture);

            return await crudService.Delete(picture.Id, saveChanges);
        }

        private async Task<Picture?> GetPictureFromContext(int id)
        {
            var picture = await context.Pictures.FindAsync(id);
            return picture;
        }

        private async Task SaveFileAndAddPathToDto(IPictureDto pictureDto, IFormFile file)
        {
            var filePath = await fileSavingService.SaveFileAsync(file);
            pictureDto.Path = filePath;
        }

        private void DeletePictureFile(Picture picture)
        {
            fileSavingService.DeleteFile(Path.GetFileName(picture.Path));
        }
    }
}
