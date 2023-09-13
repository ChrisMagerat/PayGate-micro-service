using ExampleProject.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.Infrastructure.Common;

public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(f => f.DateCreated)
            .IsRequired();
        builder.Property(f => f.CreatedBy);
        builder.Property(f => f.DateModified);
        builder.Property(f => f.ModifiedBy);
        builder.HasQueryFilter(f => !f.IsDeleted);
    }
}
