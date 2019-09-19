using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
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


        public async Task<string> UploadFileToDirectoryAsync(string directoryName, string blobName, Stream fileStream)
        {
            var blob = GetCloudBlockBlob(directoryName, blobName);
            await blob.UploadFromStreamAsync(fileStream);

            return blob.Uri.AbsolutePath;
        }

        public async Task<bool> DeleteFileFromDirectoryAsync(string directoryName, string blobName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
                throw new AppException(ExceptionEvent.InvalidParameters);
            if (string.IsNullOrWhiteSpace(blobName))
                throw new AppException(ExceptionEvent.InvalidParameters);

            return await GetCloudBlockBlob(directoryName, blobName)
                .DeleteIfExistsAsync();
        }

        public string GetDownloadLink(string directoryName, string blobName)
        {
            var blob = GetCloudBlockBlob(directoryName, blobName);

            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15),
            };
            var headers = new SharedAccessBlobHeaders
            {
                ContentDisposition = $"attachment;filename=\"{blobName}\"",
            };

            return blob.Uri.AbsoluteUri + blob.GetSharedAccessSignature(policy, headers);
        }

        private CloudBlockBlob GetCloudBlockBlob(string directoryName, string blobName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
                throw new AppException(ExceptionEvent.InvalidParameters);
            if (string.IsNullOrWhiteSpace(blobName))
                throw new AppException(ExceptionEvent.InvalidParameters);

            var directory = _blobContainer.GetDirectoryReference(directoryName);
            return directory.GetBlockBlobReference(blobName);
        }
    }
}
