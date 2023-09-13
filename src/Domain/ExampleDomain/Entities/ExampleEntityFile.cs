using ExampleProject.Domain.Common;
using ExampleProject.Domain.ExampleDomain.Enums;
using ExampleProject.Domain.ValueObjects;

namespace ExampleProject.Domain.ExampleDomain.Entities;

public class ExampleEntityFile : AuditableEntity
{
    private ExampleDomainFileType _exampleDomainFileType;
    private Guid _exampleEntityId;
    private ExampleEntity? _exampleEntity;
    private FileMetadata _metadata;
    private string _description;

    public ExampleEntityFile(FileMetadata metadata, ExampleDomainFileType exampleDomainFileType, string? description, ExampleEntity exampleEntity)
    {
        Metadata = metadata;
        ExampleDomainFileType = exampleDomainFileType;
        Id = Guid.NewGuid();
        ExampleEntity = exampleEntity;
        _description = description ?? String.Empty;
    }

    public ExampleEntityFile(FileMetadata metadata, ExampleDomainFileType exampleDomainFileType, int version, string? description, ExampleEntity exampleEntity)
    {
        Metadata = metadata;
        ExampleDomainFileType = exampleDomainFileType;
        Id = Guid.NewGuid();
        ExampleEntity = exampleEntity;
        _description = description ?? String.Empty;
    }
    public ExampleDomainFileType ExampleDomainFileType
    {
        get => _exampleDomainFileType;
        set => _exampleDomainFileType = value;
    }

    public Guid ExampleEntityId
    {
        get => _exampleEntityId;
        set => _exampleEntityId = value;
    }

    public ExampleEntity? ExampleEntity
    {
        get => _exampleEntity;
        set => _exampleEntity = value;
    }

    public FileMetadata Metadata
    {
        get => _metadata;
        set => _metadata = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string? Description
    {
        get => _description;
        set => _description = value;
    }

    public ExampleEntityFile()
    {
    }
}