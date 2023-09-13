using System.Runtime.Serialization;
using PayGate.Domain.Common;

namespace PayGate.Domain.Shared.DomainEvents;

[DataContract]
public class PasswordResetRequestedDomainEvent : DomainEvent
{
    public PasswordResetRequestedDomainEvent()
    {
    }

    public PasswordResetRequestedDomainEvent(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; set; }
}
