using PayGateMicroService.Domain.ExampleDomain.Entities;
using PayGateMicroService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PayGateMicroService.Infrastructure.ExampleDomain.Configuration;

public class ExampleEntityConfiguration: AuditableEntityConfiguration<ExampleEntity>
{
    public override void Configure(EntityTypeBuilder<ExampleEntity> builder)
    {
        // without base.Configure() method, soft deleting will not work
        base.Configure(builder);
        builder.ToTable("ExampleEntities", "ExampleEntities");
        builder.HasMany(exampleEntity => exampleEntity.Files)
            .WithOne(blob => blob.ExampleEntity);

    }
}