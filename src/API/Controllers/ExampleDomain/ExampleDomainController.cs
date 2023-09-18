using PayGateMicroService.API.Controllers.ExampleDomain.Contracts;
using PayGateMicroService.API.Controllers.Shared;
using PayGateMicroService.Application.Common;
using PayGateMicroService.Application.ExampleDomain.Commands;
using PayGateMicroService.Application.ExampleDomain.Dtos;
using PayGateMicroService.Application.ExampleDomain.Queries;
using PayGateMicroService.Application.Shared.Examples;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PayGateMicroService.API.Controllers.ExampleDomain;

[AllowAnonymous]
public class ExampleDomainController : ApiBaseController
{
    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<ExampleEntityDto>>> GetExampleEntities([FromQuery] GetExampleEntitiesContract contract, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new ExampleEntitiesQuery(contract.Pagination, contract.Search), cancellationToken));
    }

    [HttpGet("{exampleEntityId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<ExampleEntityDto>> GetExampleEntityDetails([FromRoute] Guid exampleEntityId, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new ExampleEntityQuery(exampleEntityId), cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status202Accepted)]
    public async Task<ActionResult> AddExampleEntity(AddExampleEntityContract contract, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ExampleEntityCommand(contract.AddBody), cancellationToken);
        return Accepted();
    }

    [HttpPut("{exampleEntityId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateExampleEntity([FromRoute] Guid exampleEntityId, UpdateExampleEntityContract contract, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UpdateExampleEntityCommand(contract.UpdateBody, exampleEntityId), cancellationToken);
        return Accepted();
    }

    [HttpDelete("{exampleEntityId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteExampleEntity([FromRoute] Guid exampleEntityId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteExampleEntityCommand(exampleEntityId), cancellationToken);
        return NoContent();
    }

    [HttpGet("{ExampleEntityId}/files")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<ExampleEntityFileDto>>> GetExampleEntityFiles(GetExampleEntityFilesContract contract, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(
            new ExampleEntityFilesQuery(
                contract.ExampleEntityId
                , contract.Pagination
                , contract.Search),
            cancellationToken));
    }

    [HttpPost("{ExampleEntityId:guid}/uploadFile")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult> UploadExampleEntityFile(ExampleEntityFilesContract contract,
        CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await Mediator.Send(
            new UploadFileCommand(
                contract.ExampleEntityId
                , contract.File.ToFileUpload(stream)
                , contract.ExampleDomainFileType
                , contract.Description)
            , cancellationToken);
        return NoContent();
    }

    [HttpDelete("files/{fileId:guid}")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteExampleEntityFile([FromRoute] Guid fileId,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteExampleEntityFileCommand(fileId), cancellationToken);
        return Accepted();
    }
    [AllowAnonymous]
    [HttpGet("test")]
    public async Task<ActionResult> Test([FromQuery] string data, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new Query(data), cancellationToken);
        return Ok(result);
    }

}