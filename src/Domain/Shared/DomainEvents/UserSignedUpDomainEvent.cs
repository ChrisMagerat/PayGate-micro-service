using System.Runtime.Serialization;
using PayGateMicroService.Domain.Common;

namespace PayGateMicroService.Domain.Shared.DomainEvents;

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