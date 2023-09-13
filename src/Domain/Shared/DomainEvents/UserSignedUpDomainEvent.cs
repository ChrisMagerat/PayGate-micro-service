using System.Runtime.Serialization;
using ExampleProject.Domain.Common;

namespace ExampleProject.Domain.Shared.DomainEvents;

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