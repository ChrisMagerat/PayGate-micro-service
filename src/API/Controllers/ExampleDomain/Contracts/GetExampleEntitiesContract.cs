using ExampleProject.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.API.Controllers.ExampleDomain.Contracts;

public class GetExampleEntitiesContract
{
    [FromQuery] public string? Search { get; set; }
    [FromQuery] public PaginationOptions Pagination { get; set; } = new();
}