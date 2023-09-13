using ExampleProject.Domain.ValueObjects;

namespace ExampleProject.Application.ExampleDomain.Dtos;

public class ExampleEntityFileDto
{
    public Guid Id { get; set; }
    public FileMetadataDto Metadata { get; set; }
    public string? Description { get; set; }

    public ExampleEntityFileDto(Guid id, FileMetadata metadata, string? description)
    {
        Id = id;
        Metadata = new FileMetadataDto(metadata.ContentType, metadata.FileName, metadata.Length);
        Description = description;
    }
}