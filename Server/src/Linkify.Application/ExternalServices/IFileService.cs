using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.ExternalServices
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderPath);
        Task<bool> DeleteFileAsync(string fileUrl);
        Task<string> GetFileUrlAsync(string fileName, string folderPath);
        Task<IEnumerable<string>> UploadMultipleFilesAsync(IEnumerable<(Stream FileStream, string FileName)> files, string folderPath);
        Task<bool> DeleteFolderAsync(string folderPath);
        Task<bool> DeleteMultipleFilesAsync(IEnumerable<string> fileUrls);
    }
}
