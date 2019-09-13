using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.BusinessLayer.Exceptions;
using Blazor.Server.BusinessLayer.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;

namespace Blazor.Server.BusinessLayer.Services.BlobContainerService
{
    public class BlobContainerService : IBlobContainerService
    {
        private readonly CloudBlobContainer _blobContainer;

        public BlobContainerService(IOptions<BlobContainerSettings> blobContainerOptions)
        {
            var blobContainerSettings = blobContainerOptions?.Value
                ?? throw new AppException(ExceptionEvent.InvalidParameters, "IOptions<BlobContainerSettings> not valid");

            _blobContainer = CloudStorageAccount.Parse(blobContainerSettings.ConnectionString)
                                                .CreateCloudBlobClient()
                                                .GetContainerReference(blobContainerSettings.ContainerName);
        }

        public async Task<IReadOnlyList<FileModel>> UploadFiles(IEnumerable<IFormFile> files) =>
            await Task.WhenAll(files.Select(async file =>
            {
                var blob = _blobContainer.GetBlockBlobReference(Guid.NewGuid() + "-" + file.FileName);
                await blob.UploadFromStreamAsync(file.OpenReadStream());
                return new FileModel
                {
                    Name = file.FileName,
                    Link = blob.Uri.AbsoluteUri,
                    Size = file.Length
                };
            }));
    }
}
