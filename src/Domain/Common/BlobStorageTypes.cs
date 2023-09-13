using Ardalis.SmartEnum;

namespace PayGate.Domain.Common;

public class BlobStorageTypes: SmartEnum<BlobStorageTypes>
{
    public static readonly BlobStorageTypes ExampleEntityDocuments = new ("example-entity-documents-storage", 1);
    public static readonly BlobStorageTypes ExampleEntityTwoDocuments = new ("entity-two-documents-storage", 2);

    public BlobStorageTypes(string name, int value) : base(name, value)
    {
    }
}