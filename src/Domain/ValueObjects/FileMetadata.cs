using ExampleProject.Domain.Common;

namespace ExampleProject.Domain.ValueObjects;

public class FileMetadata : ValueObject
{
    public string ContentType { get; init; }
    public string FileName { get; init; }
    public long Length { get; init; }

    public FileMetadata(string contentType, string fileName, long length)
    {
        ContentType = contentType;
        FileName = fileName;
        Length = length;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentType;
        yield return FileName;
        yield return Length;
    }
}