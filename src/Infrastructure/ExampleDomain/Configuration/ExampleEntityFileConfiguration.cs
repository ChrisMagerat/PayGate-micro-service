using ExampleProject.Domain.ExampleDomain.Entities;
using ExampleProject.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleProject.Infrastructure.ExampleDomain.Configuration;

public class ExampleEntityFileConfiguration : AuditableEntityConfiguration<ExampleEntityFile>
{
    public override void Configure(EntityTypeBuilder<ExampleEntityFile> builder)
    {
        base.Configure(builder);
        builder.ToTable($"{nameof(ExampleEntityFile)}", "ExampleEntities");
        builder.OwnsOne(blob => blob.Metadata);
    }
}