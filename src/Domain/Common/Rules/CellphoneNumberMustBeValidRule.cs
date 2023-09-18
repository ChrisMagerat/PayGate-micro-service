namespace PayGateMicroService.Domain.Common.Rules;

public class CellphoneNumberMustBeValidRule : IBusinessRule
{
    public bool IsBroken()
    {
        // TODO: Add implementation
        throw new NotImplementedException();
    }

    public string Message { get; } = "Invalid cellphone number";
}