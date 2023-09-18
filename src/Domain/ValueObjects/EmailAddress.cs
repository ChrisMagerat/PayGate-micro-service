using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.Common.Rules;

namespace PayGateMicroService.Domain.ValueObjects;

public class EmailAddress : ValueObject
{
    public string Email { get; init; }

    public EmailAddress(string email)
    {
        CheckRule(new EmailAddressMustBeValidRule(email));
        Email = email.ToLower();
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
    }
}