using PayGate.Domain.Shared;

namespace PayGate.API.Controllers.Shared;

public static class FileExtensions
{
    public static FileUpload ToFileUpload(this IFormFile file, Stream stream)
    {
        file.CopyTo(stream);
        stream.Position = 0;
        return new FileUpload(file.ContentType, file.FileName, stream, file.Length);
    }
}