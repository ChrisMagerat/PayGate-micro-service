using PayGate.Domain.Common;
using PayGate.Domain.Common.Rules;

namespace PayGate.Domain.ValueObjects;

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