using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace Blazor.Server.BusinessLayer.Services.BlobContainerService
{
    public interface IBlobContainerService
    {
        string GetDownloadLink(string directoryName, string blobName);
        Task<string> UploadFileToDirectoryAsync(string directoryName, string blobName, Stream fileStream);
        Task<bool> DeleteFileFromDirectoryAsync(string directoryName, string blobName);
    }
}
