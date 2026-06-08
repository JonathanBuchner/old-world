using System.Text.Json;
using System.Text.Json.Serialization;

namespace ow_api.Infrastructure.Services
{
    public class BlobJsonStorage : IBlobJsonStorage
    {
        private const string JsonContentType = "application/json";
        private readonly IBlobStorage _blobStorage;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public BlobJsonStorage(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
            _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public Task<T?> ReadAsync<T>(string containerName, string blobName)
        {
            return ReadAsync<T>(containerName, blobName, CancellationToken.None);
        }

        public async Task<T?> ReadAsync<T>(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var content = await _blobStorage.ReadAsync(containerName, blobName, cancellationToken);
            
            if (content == null)
                return default;

            return content.ToObjectFromJson<T>(_jsonSerializerOptions);
        }

        public Task CreateAsync<T>(string containerName, string blobName, T value)
        {
            return CreateAsync(containerName, blobName, value, CancellationToken.None);
        }

        public async Task CreateAsync<T>(string containerName, string blobName, T value, CancellationToken cancellationToken)
        {
            var binaryData = BinaryData.FromObjectAsJson(value, _jsonSerializerOptions);

            await _blobStorage.CreateAsync(containerName, blobName, binaryData, JsonContentType, cancellationToken);
        }

        public Task UpdateAsync<T>(string containerName, string blobName, T value)
        {
            return UpdateAsync(containerName, blobName, value, CancellationToken.None);
        }

        public async Task UpdateAsync<T>(string containerName, string blobName, T value, CancellationToken cancellationToken)
        {
            var binaryData = BinaryData.FromObjectAsJson(value, _jsonSerializerOptions);

            await _blobStorage.UpdateAsync(containerName, blobName, binaryData, JsonContentType, cancellationToken);
        }

        public Task DeleteAsync(string containerName, string blobName)
        {
            return DeleteAsync(containerName, blobName, CancellationToken.None);
        }

        public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            await _blobStorage.DeleteAsync(containerName, blobName, cancellationToken);
        }
    }
}
