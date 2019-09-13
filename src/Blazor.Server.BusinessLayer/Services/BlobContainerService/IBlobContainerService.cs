using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace Blazor.Server.BusinessLayer.Services.BlobContainerService
{
    public interface IBlobContainerService
    {
        Task<IReadOnlyList<FileModel>> UploadFiles(IEnumerable<IFormFile> file);
    }
}
