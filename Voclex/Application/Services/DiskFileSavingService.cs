using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
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
            var fileExtension = GetFileExtension(file);

            CheckFileExtension(fileExtension);

            CreateDirectoryIfNotExists();

            var filePath = await SaveFileToFolder(file, folderAbsolutePath);
            
            return $"{folderEndpointPath}{Path.GetFileName(filePath)}";
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

        private static async Task<string> SaveFileToFolder(IFormFile file, string folderPath)
        {
            var filePath = GetFilePath(file, folderPath);

            await SaveFileToPath(file, filePath);
            return filePath;
        }

        private static string GetFilePath(IFormFile file, string folderPath)
        {
            var fileExtension = GetFileExtension(file);

            var filePath = $"{folderPath}{Guid.NewGuid()}{fileExtension}";
            return filePath;
        }

        private static async Task SaveFileToPath(IFormFile file, string filePath)
        {
            await using var fileStream = new FileStream(filePath, FileMode.Create); //todo resize
            await file.CopyToAsync(fileStream);
        }

        private static string GetFileExtension(IFormFile file)
        {
            return Path.GetExtension(file.FileName).ToLowerInvariant();
        }
    }
}
