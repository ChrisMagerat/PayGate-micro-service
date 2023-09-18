using PayGateMicroService.Domain.Common;

namespace PayGateMicroService.Domain.ValueObjects;

public class CurrencyType : ValueObject
{
    private int Id { get; init;}
    private string Name { get; init;}
    
    public CurrencyType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
    }
}