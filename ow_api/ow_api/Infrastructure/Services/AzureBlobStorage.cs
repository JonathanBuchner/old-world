using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace ow_api.Infrastructure.Services
{
    public class AzureBlobStorage : IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task<BinaryData?> ReadAsync(string containerName, string blobName)
        {
            return ReadAsync(containerName, blobName, CancellationToken.None);
        }

        public async Task<BinaryData?> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var blobClient = GetBlobClient(containerName, blobName);

            if (!await blobClient.ExistsAsync(cancellationToken))
                return null;

            var response = await blobClient.DownloadContentAsync(cancellationToken);

            return response.Value.Content;
        }

        public Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType)
        {
            return CreateAsync(containerName, blobName, content, contentType, CancellationToken.None);
        }

        public async Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken)
        {
            var blobClient = GetBlobClient(containerName, blobName);
            var options = GetUploadOptions(contentType);

            await blobClient.UploadAsync(content, options, cancellationToken);
        }

        public Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType)
        {
            return UpdateAsync(containerName, blobName, content, contentType, CancellationToken.None);
        }

        public async Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken)
        {
            var blobClient = GetBlobClient(containerName, blobName);

            await blobClient.UploadAsync(content, overwrite: true, cancellationToken);
            await blobClient.SetHttpHeadersAsync(GetHttpHeaders(contentType), cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(string containerName, string blobName)
        {
            return DeleteAsync(containerName, blobName, CancellationToken.None);
        }

        public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var blobClient = GetBlobClient(containerName, blobName);

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        private BlobClient GetBlobClient(string containerName, string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            return containerClient.GetBlobClient(blobName);
        }

        private static BlobUploadOptions GetUploadOptions(string contentType)
        {
            return new BlobUploadOptions()
            {
                HttpHeaders = GetHttpHeaders(contentType)
            };
        }

        private static BlobHttpHeaders GetHttpHeaders(string contentType)
        {
            return new BlobHttpHeaders()
            {
                ContentType = contentType
            };
        }
    }
}
