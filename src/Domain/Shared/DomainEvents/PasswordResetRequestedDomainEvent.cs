using System.Runtime.Serialization;
using PayGateMicroService.Domain.Common;

namespace PayGateMicroService.Domain.Shared.DomainEvents;

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
