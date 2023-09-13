using System.Linq.Expressions;
using ExampleProject.Application.Common.Exceptions;
using ExampleProject.Domain.Identity.RepositoryInterfaces;
using ExampleProject.Domain.Identity.IdentityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExampleProject.Infrastructure.Identity.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public Task<User?> FindByIdAsync(Guid userId)
    {
        return _userManager.FindByIdAsync(userId.ToString());
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(user, "P@ssword1");
        if (!result.Succeeded)
        {
            throw new IdentityException("Failed to create user");
        }
    }
    
    public async Task AddUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new IdentityException("Failed to create user");
        }
    }
    
    public Task<T?> FindMappedByIdAsync<T>(Guid userId, Expression<Func<User, T>> mapping, CancellationToken cancellationToken)
    {
        return _userManager.Users.Where(user => user.Id == userId).Select(mapping).SingleOrDefaultAsync(cancellationToken);
    }
}