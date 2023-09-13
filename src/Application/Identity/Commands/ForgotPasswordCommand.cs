using Microsoft.Extensions.Logging;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Identity.RepositoryInterfaces;

namespace PayGate.Application.Identity.Commands;

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