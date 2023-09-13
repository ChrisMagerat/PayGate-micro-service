namespace ExampleProject.Domain.Common;

public abstract class DomainEvent
{
    protected DomainEvent()
    {
        
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
