using PayGateMicroService.Domain.Common;

namespace PayGateMicroService.Domain.ValueObjects;

public class CellphoneNumber : ValueObject
{
    public string CountryCode { get; init; }
    public string Number { get; init; }

    public CellphoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
    }
}