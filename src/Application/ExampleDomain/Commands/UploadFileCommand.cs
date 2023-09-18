using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Application.Shared.Services;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.ExampleDomain.Enums;
using PayGateMicroService.Domain.ExampleDomain.RepositoryInterfaces;
using PayGateMicroService.Domain.Shared;
using PayGateMicroService.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.ExampleDomain.Commands;

public class UploadFileCommand : CommandBase
{
    public Guid ExampleEntityId { get; set; }
    public FileUpload File { get; set; }
    public ExampleDomainFileType Type { get; set; }
    public string? Description { get; set; }

    public UploadFileCommand(Guid exampleEntityId, FileUpload file, ExampleDomainFileType type, string? description)
    {
        ExampleEntityId = exampleEntityId;
        File = file;
        Type = type;
        Description = description;
    }
}

public class FileUploadCommandHandler : BaseAsyncRequestHandler<UploadFileCommand>
{
    private readonly IBlobService _blobService;
    private readonly IExampleEntityRepository _exampleEntityRepository;
    private readonly IExampleEntityFilesRepository _filesRepository;

    public FileUploadCommandHandler(ILogger<FileUploadCommandHandler> logger, IExampleEntityRepository exampleEntityRepository, IBlobService blobService, IExampleEntityFilesRepository filesRepository) 
        : base(logger)
    {
        _exampleEntityRepository = exampleEntityRepository;
        _blobService = blobService;
        _filesRepository = filesRepository;
    }

    public override async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        
        // TODO: Add custom NotFoundException
        var exampleEntity =
            await _exampleEntityRepository.GetExampleEntityByIdAsync(request.ExampleEntityId, cancellationToken) 
            ?? throw new FileNotFoundException("No exampleEntities found");

        var metaData = new FileMetadata(request.File.ContentType, request.File.FileName, request.File.Length);
        var file = exampleEntity.AddDocument(metaData, request.Type, request.Description);

        await _blobService.UploadBlobAsync(
            BlobStorageTypes.ExampleEntityDocuments
            , file.Id
            , request.File.Content
            , cancellationToken);
        
        await _filesRepository.AddAsync(file, cancellationToken);

    }
}