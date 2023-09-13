using Microsoft.Extensions.Logging;
using PayGate.Application.Common.Interfaces;
using PayGate.Application.Identity.Dtos;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Identity.RepositoryInterfaces;
using PayGate.Domain.ValueObjects;

namespace PayGate.Application.Identity.Queries;

public class CurrentUserQuery : QueryBase<CurrentUserDto>
{
    public CurrentUserQuery()
    {
    }
}

public class CurrentUserQueryHandler : BaseAsyncRequestHandler<CurrentUserQuery, CurrentUserDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    
    public CurrentUserQueryHandler(ILogger<CurrentUserQueryHandler> logger, ICurrentUserService currentUserService, IUserRepository userRepository) 
        : base(logger)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public override async Task<CurrentUserDto> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = (await _userRepository.FindByIdAsync(_currentUserService.UserId) 
                      ?? throw new FileNotFoundException()
                      ).Id;

        return await _userRepository.FindMappedByIdAsync(userId
            , user => new CurrentUserDto(user.Id, new EmailAddress(user.Email))
            , cancellationToken);
    }
}