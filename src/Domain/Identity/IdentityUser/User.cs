using System.ComponentModel.DataAnnotations.Schema;
using PayGateMicroService.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using PayGateMicroService.Domain.Shared.DomainEvents;

namespace PayGateMicroService.Domain.Identity.IdentityUser;

public class User: IdentityUser<Guid>, IAuditableEntity
{
    
    public User(string email,string firstName, string lastName )
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = email;
        DomainEvents.Add(new UserSignedUpDomainEvent(Id));
    }

    private User()
    {
        
    }
    public DateTime ConfirmedDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; }
    
    public Guid CreatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DateModified { get; set; }
    [NotMapped] public List<DomainEvent> DomainEvents { get; } = new();

    public void ResetPassword()
    {
        DomainEvents.Add(new PasswordResetRequestedDomainEvent(Id));
    }
}