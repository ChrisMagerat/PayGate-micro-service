using PayGateMicroService.Domain.ExampleDomain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.ExampleDomain.Contracts;

public class ExampleEntityFilesContract
{
    [FromRoute] public Guid ExampleEntityId { get; set; }
    [FromForm] public IFormFile File { get; set; } = default!;
    [FromForm] public ExampleDomainFileType ExampleDomainFileType { get; set; }
    [FromForm] public string? Description { get; set; }
}