namespace ExampleProject.Application.Identity.Configuration;

public class EmailVerificationUrl
{
    public string BaseLink { get; }

    public EmailVerificationUrl()
    {
        
    }

    public EmailVerificationUrl(string emailConfirmationUrl)
    {
        BaseLink = emailConfirmationUrl;
    }
}