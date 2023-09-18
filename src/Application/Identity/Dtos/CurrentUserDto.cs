using PayGateMicroService.Domain.ValueObjects;

namespace PayGateMicroService.Application.Identity.Dtos;

public class CurrentUserDto
{
    public Guid Id { get; set; }
    public EmailAddress Email { get; set; }
    
    public CurrentUserDto(Guid id, EmailAddress email)
    {
        Id = id;
        Email = email;
    }
}