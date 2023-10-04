using ExampleApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleApi.Infrastructure.Persistence.Configurations;

public class ExampleConfiguration : IEntityTypeConfiguration<EntityExample>
{
      public void Configure(EntityTypeBuilder<EntityExample> builder)
      {
            builder.ToTable("EntityExample");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(e => e.Description).HasMaxLength(500).IsRequired(false);
      }
}