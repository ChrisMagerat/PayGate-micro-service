using IdentityServer4.Models;

namespace PayGateMicroService.Infrastructure.Configuration;

public static class IdentityServerConfiguration
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            },
        };

    public static IEnumerable<ApiResource> GetApiResources(AuthenticationConfiguration configuration) => new[]
    {
        new ApiResource(configuration.OAuth2.ResourceName)
        {
            Scopes = new List<string> {Scopes.AdminPortal},
            UserClaims = new List<string> {"role"}
        },
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(Scopes.AdminPortal),
        };

    public static IEnumerable<Client> GetClients(AuthenticationConfiguration configuration) =>
        new[]
        {
            new Client
            {
                ClientId = configuration.OAuth2.ClientId,
                ClientName = configuration.OAuth2.ClientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret(configuration.OAuth2.Secret.Sha256())},
                AllowedScopes =
                    {Scopes.AdminPortal, Scopes.Profile, Scopes.OpenId, Scopes.Email, Scopes.Role, Scopes.OfflineAccess},
                AllowOfflineAccess = true
            }
        };
}

public class AuthenticationConfiguration
{
    public string Authority { get; set; }
    public string Audience { get; set; }
    public OAuth2Configuration OAuth2 { get; set; }
}

public class OAuth2Configuration
{
    public string Secret { get; set; }
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string ResourceName { get; set; }
}

public static class Scopes
{
    public const string AdminPortal = "codehesion-web-dashboard";
    public const string MobileApp = "codehesion-mobile-app";
    public const string OpenId = "openid";
    public const string Profile = "profile";
    public const string Role = "role";
    public const string Email = "email";
    public const string OfflineAccess = "offline_access";
}