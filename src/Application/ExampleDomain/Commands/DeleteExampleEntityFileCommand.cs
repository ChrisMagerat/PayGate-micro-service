using ExampleProject.Application.Shared.Contracts.Mediator;
using ExampleProject.Application.Shared.Contracts.Mediator.Implementations;
using ExampleProject.Domain.ExampleDomain.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Application.ExampleDomain.Commands;

public class DeleteExampleEntityFileCommand : CommandBase
{
    public DeleteExampleEntityFileCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

public class DeleteExampleEntityFileCommandHandler : BaseAsyncRequestHandler<DeleteExampleEntityFileCommand>
{
    private readonly IExampleEntityFilesRepository _entityFileRepository;
    
    public DeleteExampleEntityFileCommandHandler(ILogger<DeleteExampleEntityFileCommandHandler> logger, IExampleEntityFilesRepository entityFileRepository) : base(logger)
    {
        _entityFileRepository = entityFileRepository;
    }

    public override async Task Handle(DeleteExampleEntityFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _entityFileRepository.GetBlobMetaDataByIdAsync(request.Id, cancellationToken) ?? throw new FileNotFoundException("File not found");
        file.Delete();
    }
}