using PayGateMicroService.Domain.ValueObjects;

namespace PayGateMicroService.Domain.Shared;

public class FileUpload : FileMetadata
{
    public Stream Content { get; set; }
    
    public FileUpload(string contentType, string fileName, Stream content, long length)
        :base(contentType, fileName, length)
    { 
        Content = content;
    }
}