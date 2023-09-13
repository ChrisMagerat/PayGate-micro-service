using System.Runtime.Serialization;
using PayGate.Domain.Common;

namespace PayGate.Domain.Shared.DomainEvents;

[DataContract]
public class UserSignedUpDomainEvent : DomainEvent
{
    public UserSignedUpDomainEvent()
    {
        
    }

    public UserSignedUpDomainEvent(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}