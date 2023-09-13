using PayGate.Domain.ValueObjects;

namespace PayGate.Domain.Shared;

public class FileUpload : FileMetadata
{
    public Stream Content { get; set; }
    
    public FileUpload(string contentType, string fileName, Stream content, long length)
        :base(contentType, fileName, length)
    { 
        Content = content;
    }
}