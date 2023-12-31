using System.Security.Claims;
using ExampleProject.Application.Common.Interfaces;

namespace ExampleProject.API.Contracts;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var nameIdentifier = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                                 Guid.Empty.ToString();
            //If we need
            var userId = Guid.Parse(nameIdentifier);
            return userId;
        }
    }
}
