namespace PayGate.Domain.Common.Rules;

public class LuhnAlgorithmMustPassRule : IBusinessRule
{
    private string _data { get; set; }

    public LuhnAlgorithmMustPassRule(string data)
    {
        _data = data;
    }
    
    private bool LuhnCheck()
    {
        if (string.IsNullOrWhiteSpace(_data)) 
            return false;
        
        _data = _data.Replace(" ", "").Replace("-", "");

        var sum = 0;
        var alternate = false;

        for (var i = _data.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(_data[i]))
            {
                return false;
            }

            var digit = _data[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }
        
        return sum % 10 == 0;
    }

    public bool IsBroken()
    {
        return LuhnCheck();
    }

    public string Message { get; } = "Luhn algorithm did not pass";
}