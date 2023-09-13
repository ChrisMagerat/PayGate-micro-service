using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PayGate.Application.Common.Exceptions;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Identity.IdentityUser;
using PayGate.Domain.Identity.RepositoryInterfaces;

namespace PayGate.Application.Identity.Commands;

public class VerifyEmailCommand : CommandBase
{
    public VerifyEmailCommand(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; }
    public string Token { get; }
}

public class VerifyEmailCommandHandler : BaseAsyncRequestHandler<VerifyEmailCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public VerifyEmailCommandHandler(ILogger<VerifyEmailCommandHandler> logger, IUserRepository userRepository, UserManager<User> userManager) 
        : base(logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public override async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        var decodedToken = DecodeToken(request.Token);

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }

        user.ConfirmedDate = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
    }
    
    private static string DecodeToken(string token)
    {
        var bytes = Convert.FromBase64String(token);
        return Encoding.ASCII.GetString(bytes);
    }
}