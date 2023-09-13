using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PayGate.Application.Identity.Configuration;
using PayGate.Application.Shared.Contracts.Mediator;
using PayGate.Application.Shared.Contracts.Mediator.Implementations;
using PayGate.Application.Shared.Services;
using PayGate.Domain.Identity.IdentityUser;
using PayGate.Domain.Identity.RepositoryInterfaces;

namespace PayGate.Application.Identity.Commands;

public class SignUpEmailVerificationCommand : CommandBase
{
    public SignUpEmailVerificationCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}

public class SignUpEmailVerificationCommandHandler : BaseAsyncRequestHandler<SignUpEmailVerificationCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly HostUrl _hostUrl;
    private readonly EmailVerificationUrl _emailVerificationUrl;
    private readonly ILinkGeneratorService _linkGeneratorService;
    
    private User? _user;

    public SignUpEmailVerificationCommandHandler(ILogger<SignUpEmailVerificationCommandHandler> logger, IUserRepository userRepository, UserManager<User> userManager, EmailVerificationUrl emailVerificationUrl, HostUrl hostUrl, ILinkGeneratorService linkGeneratorService) 
        : base(logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _emailVerificationUrl = emailVerificationUrl;
        _hostUrl = hostUrl;
        _linkGeneratorService = linkGeneratorService;
    }

    public override async Task Handle(SignUpEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        _user = await _userRepository.FindByIdAsync(request.UserId);
        var encodedToken = await GenerateToken();
        var queryParams = new
        {
            token = encodedToken,
            email = HttpUtility.UrlEncode(_user.Email)
        };
        var verificationLink = _linkGeneratorService.GenerateLink(_hostUrl.BaseLink, _emailVerificationUrl.BaseLink, queryParams);
        await SendAccountVerificationEmailAsync(verificationLink);
    }

    private async Task<string> GenerateToken()
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
        var encodedToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(token));
        return encodedToken;
    }

    private async Task SendAccountVerificationEmailAsync(string link)
    {
        // TODO: implement email sending
        Console.WriteLine(link);
        //throw new FileNotFoundException($"Please implement email sending first, data for testing: {link}");
    }
}