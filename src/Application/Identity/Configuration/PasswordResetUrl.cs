namespace PayGate.Application.Identity.Configuration;

public class PasswordResetUrl
{
    public string BaseLink { get; }

    public PasswordResetUrl()
    {
        
    }

    public PasswordResetUrl(string passwordResetUrl)
    {
        BaseLink = passwordResetUrl;
    }
}