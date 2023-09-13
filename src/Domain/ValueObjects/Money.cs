using ExampleProject.Domain.Common;
using ExampleProject.Domain.Common.Rules;

namespace ExampleProject.Domain.ValueObjects;

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