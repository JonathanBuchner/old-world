namespace ow_api.Infrastructure.Services
{
    public interface IBlobStorage
    {
        Task<BinaryData?> ReadAsync(string containerName, string blobName);
        Task<BinaryData?> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken);
        Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType);
        Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken);
        Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType);
        Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, string blobName);
        Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken);
    }
}
