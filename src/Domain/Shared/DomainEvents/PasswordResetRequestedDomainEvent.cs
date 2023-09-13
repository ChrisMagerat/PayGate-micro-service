using System.Runtime.Serialization;
using ExampleProject.Domain.Common;

namespace ExampleProject.Domain.Shared.DomainEvents;

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
