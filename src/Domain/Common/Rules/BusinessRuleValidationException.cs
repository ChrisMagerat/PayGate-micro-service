using System.Runtime.Serialization;

namespace PayGate.Domain.Common.Rules;

[Serializable]
public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BrokenRule { get; }
    public string Details { get; }

    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(message: brokenRule.Message)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.Message;
    }

    protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Details = info.GetString("AdditionalData");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("Details", Details);
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}