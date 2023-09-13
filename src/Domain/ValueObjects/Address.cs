using PayGate.Domain.Common;

namespace PayGate.Domain.ValueObjects;

public class Address : ValueObject
{
    
    public string? UnitNo { get; init; }
    public string? ComplexName { get; init; }
    public string StreetNo { get; init; }
    public string StreetName { get; init; }
    public string Suburb { get; init; }
    public string City { get; init; }
    public string Province { get; init; }
    public string PostalCode { get; init; }
    public bool IsBusinessAddress { get; init; }

    public Address(string streetNo
        , string streetName
        , string suburb
        , string city
        , string province
        , string postalCode
        , bool isBusinessAddress
        , string? unitNo = ""
        , string? complexName = "")
    {
        StreetNo = streetNo;
        StreetName = streetName;
        Suburb = suburb;
        City = city;
        Province = province;
        PostalCode = postalCode;
        IsBusinessAddress = isBusinessAddress;
        UnitNo = unitNo;
        ComplexName = complexName;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UnitNo ?? "";
        yield return ComplexName ?? "";
        yield return StreetNo;
        yield return StreetName;
        yield return Suburb;
        yield return City;
        yield return Province;
        yield return PostalCode;
        yield return IsBusinessAddress;
    }
}