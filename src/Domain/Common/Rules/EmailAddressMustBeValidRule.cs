using System.Net.Mail;

namespace ExampleProject.Domain.Common.Rules;

public class EmailAddressMustBeValidRule : IBusinessRule
{
    private string _email { get; init; }

    public EmailAddressMustBeValidRule(string email)
    {
        _email = email;
    }
    public bool IsBroken()
    {
        try
        {
            _ = new MailAddress(_email);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string Message { get; } = "Invalid email address";
}