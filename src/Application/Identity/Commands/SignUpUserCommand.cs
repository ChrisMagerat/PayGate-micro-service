using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Domain.Identity.IdentityUser;
using PayGateMicroService.Domain.Identity.RepositoryInterfaces;
using PayGateMicroService.Domain.Shared.DomainEvents;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.Identity.Commands;

public class SignUpUserCommand : CommandBase
{
    public SignUpUserCommand(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
}

public class SignUpUserCommandHandler : BaseAsyncRequestHandler<SignUpUserCommand>
{
    private readonly IUserRepository _userRepository;

    public SignUpUserCommandHandler(ILogger<SignUpUserCommandHandler> logger, IUserRepository userRepository) 
        : base(logger)
    {
        _userRepository = userRepository;
    }

    public override async Task Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Email, request.FirstName, request.LastName);
        await _userRepository.AddUserAsync(user, request.Password, cancellationToken);
    }
}