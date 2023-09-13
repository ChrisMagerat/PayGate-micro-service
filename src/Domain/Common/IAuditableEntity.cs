namespace PayGate.Domain.Common;

public interface IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DateModified { get; set; }
    List<DomainEvent> DomainEvents { get; }
}
