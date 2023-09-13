using System.Globalization;
using PayGate.Domain.Common;
using PayGate.Domain.Common.Rules;

namespace PayGate.Domain.ValueObjects;

public class SaIdNumber : ValueObject
{
    public string IdNumber { get; init; }

    public SaIdNumber(string idNumber)
    {
        CheckRule(new SaIdNumberMustBeValidRule(idNumber));
        CheckRule(new LuhnAlgorithmMustPassRule(idNumber));
        IdNumber = idNumber;
    }
    
    public DateTime GetDateOfBirth()
    {
        var datePart = IdNumber.Remove(6, 7);
        return DateTime.ParseExact(datePart, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    public string GetGender()
    {
        var genderPart = IdNumber.Remove(0, 6).Remove(4, 3);
        return int.Parse(genderPart) > 5000 ? "male" : "female";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IdNumber;
    }
}