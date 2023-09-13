using Microsoft.AspNetCore.Mvc;
using PayGate.Domain.ExampleDomain.Enums;

namespace PayGate.API.Controllers.ExampleDomain.Contracts;

public class ExampleEntityFilesContract
{
    [FromRoute] public Guid ExampleEntityId { get; set; }
    [FromForm] public IFormFile File { get; set; } = default!;
    [FromForm] public ExampleDomainFileType ExampleDomainFileType { get; set; }
    [FromForm] public string? Description { get; set; }
}