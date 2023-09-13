using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ExampleProject.Application.Shared.Services;
using ExampleProject.Domain.Common;

namespace ExampleProject.Infrastructure.Shared.Services;

public class AzureBlobService: IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    public AzureBlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task UploadBlobAsync(BlobStorageTypes container, Guid key, Stream fileStream,
        CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName: container.Name);
        await containerClient.UploadBlobAsync(blobName: $"{key}", content: fileStream, cancellationToken);
    }

    public async Task DeleteBlobAsync(BlobStorageTypes container, Guid key,
        CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName: container.Name);
        await containerClient.DeleteBlobIfExistsAsync(blobName: $"{key}", snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
    }

    public async Task ReadBlobIntoStream(BlobStorageTypes container, Guid key, Stream stream,
        CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName: container.Name);
        var blobClient = containerClient.GetBlobClient(blobName: $"{key}");
        await blobClient.DownloadToAsync(destination: stream, cancellationToken);
    }
}