using System.Text;
using System.Web;
using PayGateMicroService.Application.Identity.Configuration;
using PayGateMicroService.Application.Shared.Contracts.Mediator;
using PayGateMicroService.Application.Shared.Contracts.Mediator.Implementations;
using PayGateMicroService.Application.Shared.Services;
using PayGateMicroService.Domain.Identity.IdentityUser;
using PayGateMicroService.Domain.Identity.RepositoryInterfaces;
using PayGateMicroService.Infrastructure.Configuration.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace PayGateMicroService.Application.Identity.Commands;

public class SendPasswordResetCommand : CommandBase
{
    public SendPasswordResetCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}

public class SendPasswordResetCommandHandler : BaseAsyncRequestHandler<SendPasswordResetCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly HostUrl _hostUrl;
    private readonly PasswordResetUrl _passwordResetUrl;
    private readonly ILinkGeneratorService _linkGeneratorService;
    private User? _user;

    public SendPasswordResetCommandHandler(ILogger<SendPasswordResetCommandHandler> logger
        , IUserRepository userRepository
        , UserManager<User> userManager
        , PasswordResetUrl passwordResetUrl
        , ILinkGeneratorService linkGeneratorService
        , HostUrl hostUrl)
        : base(logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _passwordResetUrl = passwordResetUrl;
        _linkGeneratorService = linkGeneratorService;
        _hostUrl = hostUrl;
    }

    public override async Task Handle(SendPasswordResetCommand request, CancellationToken cancellationToken)
    {
        _user = await _userRepository.FindByIdAsync(request.UserId);
        var encodedToken = await GenerateToken();
        var queryParams = new
        {
            token = encodedToken,
            email = HttpUtility.UrlEncode(_user.Email)
        };
        var verificationLink = _linkGeneratorService.GenerateLink(_hostUrl.BaseLink, _passwordResetUrl.BaseLink, queryParams);
        await SendPasswordResetEmailAsync(verificationLink);
    }

    private async Task<string> GenerateToken()
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
        var encodedToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(token));
        return encodedToken;
    }
    
    private async Task SendPasswordResetEmailAsync(string link)
    {
        // TODO: implement email sending
    }
}