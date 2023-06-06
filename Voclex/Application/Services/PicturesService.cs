using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Application.Services
{
    public sealed class PicturesService
    {
        private readonly ICrudService<Picture, IPictureDto> crudService;
        private readonly IFileSavingService fileSavingService;
        private readonly IDbContext context;
        private readonly HttpClient httpClient;

        public PicturesService(
	        ICrudService<Picture, IPictureDto> crudService, 
            IFileSavingServiceFactory fileSavingServiceFactory, 
            IDbContext context, HttpClient httpClient)
        {
            this.crudService = crudService;
            this.context = context;
            this.httpClient = httpClient;
            this.fileSavingService = fileSavingServiceFactory.GetPictureFileSavingService();
        }

        public async Task<Picture> Create(IPictureDto pictureDto, IFormFile file, bool saveChanges = true)
        {
            await SaveFileAndAddPathToDto(pictureDto, file);
            return await crudService.Create(pictureDto, saveChanges);
        }

        public async Task<Picture> Create(IPictureDto pictureDto, string pictureUrl, bool saveChanges = true)
        {
            await DownloadAndSaveFileAddPathToDto(pictureDto, pictureUrl);
            return await crudService.Create(pictureDto, saveChanges);
        }

        public async Task<IPictureDto?> GetByIdAsync(int id)
        {
            return await crudService.GetByIdAsync(id);
        }

        public async Task<Picture?> UpdateAsync(IPictureDto pictureDto, string pictureUrl, bool saveChanges = true)
        {
	        if (!await PictureExistsThenDelete(pictureDto)) return null;

	        await DownloadAndSaveFileAddPathToDto(pictureDto, pictureUrl);

	        return await crudService.UpdateAsync(pictureDto, saveChanges);
		}


		public async Task<Picture?> UpdateAsync(IPictureDto pictureDto, IFormFile file, bool saveChanges = true)
        {
            if (!await PictureExistsThenDelete(pictureDto)) return null;

            await SaveFileAndAddPathToDto(pictureDto, file);

            return await crudService.UpdateAsync(pictureDto, saveChanges);
        }

        private async Task<bool> PictureExistsThenDelete(IPictureDto pictureDto)
        {
	        var modelFromDb = await GetPictureFromContext(pictureDto.Id);
	        if (modelFromDb == null) return false;

	        DeletePictureFile(modelFromDb);
	        return true;
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

        private async Task DownloadAndSaveFileAddPathToDto(IPictureDto pictureDto, string pictureUrl)
        {
            var url = new Uri(pictureUrl);
            var pictureName = url.LocalPath.Substring(url.LocalPath.LastIndexOf('/') + 1);

            await using var imageStream = await httpClient.GetStreamAsync(url);
            var filePath = await fileSavingService.SaveFileAsync(imageStream, pictureName);

            pictureDto.Path = filePath;
        }

        private void DeletePictureFile(Picture picture)
        {
            fileSavingService.DeleteFile(Path.GetFileName(picture.Path));
        }
    }
}
