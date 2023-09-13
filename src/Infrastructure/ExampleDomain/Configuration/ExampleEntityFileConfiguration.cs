using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PayGate.Domain.ExampleDomain.Entities;
using PayGate.Infrastructure.Common;

namespace PayGate.Infrastructure.ExampleDomain.Configuration;

public class ExampleEntityFileConfiguration : AuditableEntityConfiguration<ExampleEntityFile>
{
    public override void Configure(EntityTypeBuilder<ExampleEntityFile> builder)
    {
        base.Configure(builder);
        builder.ToTable($"{nameof(ExampleEntityFile)}", "ExampleEntities");
        builder.OwnsOne(blob => blob.Metadata);
    }
}