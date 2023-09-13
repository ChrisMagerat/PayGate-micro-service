using System.Linq.Expressions;
using PayGate.Domain.Identity.IdentityUser;

namespace PayGate.Domain.Identity.RepositoryInterfaces;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(Guid userId);
    Task<User?> FindByEmailAsync(string email);
    Task AddUserAsync(User user, CancellationToken cancellationToken);
    Task AddUserAsync(User user, string password, CancellationToken cancellationToken);
    Task<T?> FindMappedByIdAsync<T>(Guid userId, Expression<Func<User, T>> mapping, CancellationToken cancellationToken);

}