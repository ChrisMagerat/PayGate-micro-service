using System.ComponentModel.DataAnnotations.Schema;
using ExampleProject.Domain.Common.Rules;

namespace ExampleProject.Domain.Common;

public abstract class AuditableEntity : IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public Guid? ModifiedBy { get; set; }
    public DateTime? DateModified { get; set; }
    public bool IsDeleted { get; private set; } = false;

    [NotMapped] public List<DomainEvent> DomainEvents { get; } = new();
    protected void AddDomainEvent(DomainEvent domainEvent) => DomainEvents.Add(domainEvent);

    public void Delete() => IsDeleted = true;
    
    protected static void CheckRule(IBusinessRule rule)
    {
        if(rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
