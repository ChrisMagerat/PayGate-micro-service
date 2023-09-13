using Microsoft.Extensions.Logging;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Identity.IdentityUser;
using PayGate.Domain.Identity.RepositoryInterfaces;

namespace PayGate.Application.Identity.Commands;

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