using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PayGate.Application.Common.Exceptions;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Domain.Identity.IdentityUser;
using PayGate.Domain.Identity.RepositoryInterfaces;

namespace PayGate.Application.Identity.Commands;

public class ResetPasswordCommand : CommandBase
{
    public ResetPasswordCommand(string email, string token, string password)
    {
        Email = email;
        Token = token;
        Password = password;
    }

    public string Email { get; }
    public string Token { get; }
    public string Password { get; }
}

public class ResetPasswordCommandHandler : BaseAsyncRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public ResetPasswordCommandHandler(ILogger<ResetPasswordCommandHandler> logger, UserManager<User> userManager, IUserRepository userRepository) 
        : base(logger)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public override async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new IdentityException("Token could not be verified");
        }

        var decodedToken = DecodeToken(request.Token);
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.Password);
        
        if (!result.Succeeded)
        {
            throw new IdentityException(result.Errors);
        }
        
        await _userManager.UpdateAsync(user);
    }
    
    private static string DecodeToken(string token)
    {
        var bytes = Convert.FromBase64String(token);
        return Encoding.ASCII.GetString(bytes);
    }    
    
}