using ExampleApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExampleApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
      public ApplicationDbContext(DbContextOptions options) : base(options)
      {
      }

      public DbSet<EntityExample> EntityExamples { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
      }
}