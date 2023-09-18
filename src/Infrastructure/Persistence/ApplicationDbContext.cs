using System.Reflection;
using PayGateMicroService.Application.Common.Interfaces;
using PayGateMicroService.Domain.Common;
using PayGateMicroService.Domain.Identity.IdentityUser;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PayGateMicroService.Domain.ExampleDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PayGateMicroService.Infrastructure.Persistence;

public class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IPersistedGrantDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly OperationalStoreOptions _operationalStoreOptions;
    public DbSet<ExampleEntity> ExampleEntities { get; set; }
    public DbSet<ExampleEntityFile> ExampleEntityFiles { get; set; }

    public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, OperationalStoreOptions operationalStoreOptions) : base(options)
    {
        _currentUserService = currentUserService;
        _operationalStoreOptions = operationalStoreOptions;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ConfigurePersistedGrantContext(_operationalStoreOptions);
    }

    public override int SaveChanges()
    {
        SetAuditableState();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetAuditableState();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetAuditableState();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        SetAuditableState();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetAuditableState()
    {
         foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
         {
             switch (entry.State)
             {
                 case EntityState.Added:
                     entry.Entity.CreatedBy = _currentUserService.UserId;
                     break;
                 case EntityState.Deleted:
                     entry.Entity.Delete();
                     entry.State = EntityState.Modified;
                     entry.Entity.ModifiedBy = _currentUserService.UserId;
                     entry.Entity.DateModified = DateTime.UtcNow;
                     break;
                 case EntityState.Modified:
                     entry.Entity.ModifiedBy = _currentUserService.UserId;
                     entry.Entity.DateModified = DateTime.UtcNow;
                     break;
             }
         }
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public DbSet<PersistedGrant> PersistedGrants { get; set; }
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
}
