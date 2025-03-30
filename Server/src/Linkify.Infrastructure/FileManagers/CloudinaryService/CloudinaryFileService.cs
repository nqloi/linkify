using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Linkify.Application.ExternalServices;
using Microsoft.Extensions.Options;

namespace Linkify.Infrastructure.FileManagers.CloudinaryService
{
    public class CloudinaryFileService : IFileService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryFileService(IOptions<CloudinarySettings> options)
        {
            var settings = options.Value; ;

            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderPath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = folderPath
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            var publicId = GetPublicIdFromUrl(fileUrl);
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }

        public Task<string> GetFileUrlAsync(string fileName, string folderPath)
        {
            return Task.FromResult($"{_cloudinary.Api.UrlImgUp.BuildUrl(folderPath + "/" + fileName)}");
        }

        public async Task<IEnumerable<string>> UploadMultipleFilesAsync(IEnumerable<(Stream FileStream, string FileName)> files, string folderPath)
        {
            // Use Task.WhenAll to upload files concurrently
            var uploadTasks = files.Select(async file =>
            {
                return await UploadFileAsync(file.FileStream, file.FileName, folderPath);
            });

            return await Task.WhenAll(uploadTasks);
        }

        private string GetPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;
            return string.Join("", segments.Skip(2)).Split('.')[0];
        }

        public async Task<bool> DeleteMultipleFilesAsync(IEnumerable<string> fileUrls)
        {
            var deletionTasks = fileUrls.Select(async fileUrl =>
            {
                var publicId = GetPublicIdFromUrl(fileUrl);
                var deletionParams = new DeletionParams(publicId);
                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                return deletionResult.Result == "ok";
            });

            var results = await Task.WhenAll(deletionTasks);

            return results.All(result => result);
        }

        public async Task<bool> DeleteFolderAsync(string folderPath)
        {
            await _cloudinary.DeleteResourcesByPrefixAsync(folderPath);
            var listResult = await _cloudinary.DeleteFolderAsync(folderPath);

            return listResult.Deleted.Count > 0;
        }
    }
}
