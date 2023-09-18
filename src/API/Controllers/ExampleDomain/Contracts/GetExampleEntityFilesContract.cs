using PayGateMicroService.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.ExampleDomain.Contracts;

public class GetExampleEntityFilesContract
{
    [FromRoute] public Guid ExampleEntityId { get; set; }
    [FromQuery] public string? Search { get; set; }
    [FromQuery] public PaginationOptions Pagination { get; set; } = new();
}