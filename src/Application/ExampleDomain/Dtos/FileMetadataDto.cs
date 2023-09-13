namespace ExampleProject.Application.ExampleDomain.Dtos;

public class FileMetadataDto
{
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public long Length { get; set; }

    public FileMetadataDto(string contentType, string fileName, long length)
    {
        ContentType = contentType;
        FileName = fileName;
        Length = length;
    }
}