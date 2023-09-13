using PayGate.Domain.Common;
using PayGate.Domain.ExampleDomain.Enums;
using PayGate.Domain.ValueObjects;

namespace PayGate.Domain.ExampleDomain.Entities;

public class ExampleEntity : AuditableEntity
{
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<ExampleEntityFile>? Files { get; set; } = new List<ExampleEntityFile>();

    public ExampleEntity(string name, DateTime registrationDate)
    {
        Name = name;
        RegistrationDate = registrationDate;
    }
    
    public ExampleEntityFile AddDocument(FileMetadata fileMetaData, ExampleDomainFileType exampleDomainFileType, string? description)
    {
        var existingFile = Files?.FirstOrDefault(file => file.ExampleDomainFileType == exampleDomainFileType);

        if (existingFile is null)
        {
            var file = new ExampleEntityFile(fileMetaData, exampleDomainFileType, description, this);
            Files!.Add(file);
            return file;
        }

        existingFile.Metadata = fileMetaData;
        existingFile.Description = description;
        return existingFile;
    }
}