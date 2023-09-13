using Azure.Storage.Blobs;
using ExampleProject.Domain.Common;

namespace ExampleProject.Infrastructure.Shared.Configuration;

public class BlobContainerInitializer
{
    private readonly string _connectionString;

    public BlobContainerInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeContainerAsync(CancellationToken cancellationToken)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);

        var tasks = BlobStorageTypes.List.Select(container =>

            blobServiceClient
                .GetBlobContainerClient(container.Name)
                .CreateIfNotExistsAsync(cancellationToken: cancellationToken));

        await Task.WhenAll(tasks);
    }
}