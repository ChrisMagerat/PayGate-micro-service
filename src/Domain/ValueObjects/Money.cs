using PayGate.Domain.Common;
using PayGate.Domain.Common.Rules;

namespace PayGate.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Value { get; init; }
    public CurrencyType Currency { get; init; }

    public Money(decimal value, CurrencyType currency)
    {
        CheckRule(new MoneyMustBePositiveRule(value));
        Value = value;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}