using Microsoft.AspNetCore.Identity;

namespace ExampleProject.Application.Common.Exceptions;

public class IdentityException : Exception
{
    public IEnumerable<IdentityError> Errors { get; set; }

    public IdentityException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public IdentityException(string message): base(message)
    {
        
    }
}