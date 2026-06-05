namespace ow_api.Infrastructure.Services
{
    public interface IBlobJsonStorage
    {
        Task<T?> ReadAsync<T>(string containerName, string blobName);
        Task<T?> ReadAsync<T>(string containerName, string blobName, CancellationToken cancellationToken);
        Task CreateAsync<T>(string containerName, string blobName, T value);
        Task CreateAsync<T>(string containerName, string blobName, T value, CancellationToken cancellationToken);
        Task UpdateAsync<T>(string containerName, string blobName, T value);
        Task UpdateAsync<T>(string containerName, string blobName, T value, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, string blobName);
        Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken);
    }
}
