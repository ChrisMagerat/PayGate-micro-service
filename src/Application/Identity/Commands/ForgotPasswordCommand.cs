using ExampleProject.Application.Common.Exceptions;
using ExampleProject.Application.Shared.Contracts.Mediator;
using ExampleProject.Application.Shared.Contracts.Mediator.Implementations;
using ExampleProject.Domain.Identity.RepositoryInterfaces;
using ExampleProject.Domain.Shared.DomainEvents;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Application.Identity.Commands;

public class ForgotPasswordCommand : CommandBase
{
    public ForgotPasswordCommand(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}

public class ForgotPasswordCommandHandler : BaseAsyncRequestHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;

    public ForgotPasswordCommandHandler(ILogger<ForgotPasswordCommandHandler> logger, IUserRepository userRepository) : base(logger)
    {
        _userRepository = userRepository;
    }

    public override async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        if(user is not null)
        {
            user.ResetPassword();
        }
    }
}