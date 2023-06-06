using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public sealed class DiskFileSavingService : IFileSavingService
    {
        private readonly string[] picturesExtensions;
        private readonly string folderAbsolutePath;
        private readonly string folderEndpointPath;

        public DiskFileSavingService(string[] picturesExtensions, string folderAbsolutePath, string folderEndpointPath)
        {
            this.picturesExtensions = picturesExtensions;
            this.folderAbsolutePath = folderAbsolutePath;
            this.folderEndpointPath = folderEndpointPath;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
	        await using var formFileStream = file.OpenReadStream();
			return await SaveFileAsync(formFileStream, file.FileName);
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            CheckFileExtensionAndCreateDirectoryIfNotExists(fileName);

            return await SaveFileToFolderAndGetRelativePath(fileStream, fileName);
        }

        public void DeleteFile(string fileName)//todo async?
        {
            File.Delete($"{folderAbsolutePath}{fileName}"); 
        }


        private void CheckFileExtensionAndCreateDirectoryIfNotExists(string fileFileName)
        {
            var fileExtension = GetFileExtension(fileFileName);

            CheckFileExtension(fileExtension);

            CreateDirectoryIfNotExists();
        }

        private void CheckFileExtension(string fileExtension)
        {
            if (!picturesExtensions.Contains(fileExtension))
                throw new ArgumentException(
                    $"This file format is not supported. Only pictures of those formats are allowed: {string.Join(", ", picturesExtensions)}");
        }

        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(folderAbsolutePath))
                Directory.CreateDirectory(folderAbsolutePath);
        }

        private async Task<string> SaveFileToFolderAndGetRelativePath(Stream formFileStream, string fileName)
        {
            var filePath = await SaveFileToFolder(formFileStream, fileName, folderAbsolutePath);

            return $"{folderEndpointPath}{Path.GetFileName(filePath)}";
        }

        private static async Task<string> SaveFileToFolder(Stream formFileStream, string fileName, string folderPath)
        {
            var filePath = GetFilePath(folderPath, fileName);

            await SaveFileToPath(filePath, formFileStream);
            return filePath;
        }

        private static string GetFilePath(string folderPath, string fileName)
        {
            var fileExtension = GetFileExtension(fileName);

            var filePath = $"{folderPath}{Guid.NewGuid()}{fileExtension}";
            return filePath;
        }

        private static async Task SaveFileToPath(string filePath, Stream formFileStream)
        {
            await using var fileStream = new FileStream(filePath, FileMode.Create); //todo resize
            await formFileStream.CopyToAsync(fileStream);
        }

        private static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLowerInvariant();
        }
    }
}
