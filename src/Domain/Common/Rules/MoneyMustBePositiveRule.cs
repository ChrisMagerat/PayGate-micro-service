namespace ExampleProject.Domain.Common.Rules;

public class MoneyMustBePositiveRule : IBusinessRule
{
    private readonly decimal _amount;

    public MoneyMustBePositiveRule(decimal amount)
    {
        _amount = amount;
    }

    public bool IsBroken() => _amount < 0;

    public string Message { get; } = "Money must be positive";
}