using PayGate.Domain.Common;

namespace PayGate.Application.Shared.Services;

public interface IBlobService
{
    Task UploadBlobAsync(BlobStorageTypes container, Guid key, Stream fileStream,
        CancellationToken cancellationToken);

    Task DeleteBlobAsync(BlobStorageTypes container, Guid key, CancellationToken cancellationToken);
    Task ReadBlobIntoStream(BlobStorageTypes container, Guid key, Stream stream,
        CancellationToken cancellationToken);
}
