using System.Globalization;

namespace ExampleProject.Domain.Common.Rules;

public class SaIdNumberMustBeValidRule : IBusinessRule
{
    private string _idNumber { get; set; }

    public SaIdNumberMustBeValidRule(string idNumber)
    {
        _idNumber = idNumber;
    }
    public bool IsBroken()
    {
        return LengthCheck() 
                && CitizenCheck() 
                && DateCheck();
    }

    public string Message { get; private set; }
    
    private bool LengthCheck()
    {
        if (_idNumber.Length == 13) 
            return true;
        
        Message = "Length must be exactly 13 characters";
        return false;
    }
    private bool CitizenCheck()
    {
        var code = _idNumber.Remove(0, 10).Remove(1, 2);

        if (code is "0" or "1" or "2") 
            return true;
        
        Message = "Citizen code is not valid";
        return false;
    }
    private bool DateCheck()
    {   
        var dateString = _idNumber.Remove(6, 7);
        if (DateTime.TryParseExact(dateString, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            return true;
        
        Message = "Date of birth is not valid";
        return false;
    }
    
}