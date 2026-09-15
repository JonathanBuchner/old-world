using OldWorld.Infrastructure.Storage;

namespace OldWorld.Api.Tests;

internal sealed class InMemoryBlobStorage : IBlobStorage
{
    public Dictionary<(string Container, string Blob), BinaryData> Blobs { get; } = [];
    public CancellationToken LastCancellationToken { get; private set; }
    public string? LastContentType { get; private set; }

    public Task<BinaryData?> ReadAsync(string containerName, string blobName) => ReadAsync(containerName, blobName, CancellationToken.None);

    public Task<BinaryData?> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken)
    {
        LastCancellationToken = cancellationToken;
        cancellationToken.ThrowIfCancellationRequested();
        Blobs.TryGetValue((containerName, blobName), out var content);
        return Task.FromResult(content);
    }

    public Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType) => CreateAsync(containerName, blobName, content, contentType, CancellationToken.None);
    public Task CreateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken) => UpdateAsync(containerName, blobName, content, contentType, cancellationToken);
    public Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType) => UpdateAsync(containerName, blobName, content, contentType, CancellationToken.None);

    public Task UpdateAsync(string containerName, string blobName, BinaryData content, string contentType, CancellationToken cancellationToken)
    {
        LastCancellationToken = cancellationToken;
        LastContentType = contentType;
        cancellationToken.ThrowIfCancellationRequested();
        Blobs[(containerName, blobName)] = content;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string containerName, string blobName) => DeleteAsync(containerName, blobName, CancellationToken.None);

    public Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
    {
        LastCancellationToken = cancellationToken;
        cancellationToken.ThrowIfCancellationRequested();
        Blobs.Remove((containerName, blobName));
        return Task.CompletedTask;
    }
}
