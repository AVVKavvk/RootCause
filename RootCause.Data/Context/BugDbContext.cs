using Microsoft.EntityFrameworkCore;
using RootCause.Core.Entities;

namespace RootCause.Data.Context;

public class BugDBContext : DbContext
{
    public DbSet<Bug> Bugs { get; set; }

    public BugDBContext(DbContextOptions<BugDBContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bug>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.ErrorMessage).IsRequired();
            entity.Property(e => e.RootCause).IsRequired();
            entity.Property(e => e.Fix).IsRequired();
            entity.Property(e => e.TimeToSolve).IsRequired();
            entity.Property(e => e.StackTags).IsRequired();
            entity.Property(e => e.ResolvedAt).IsRequired();
            entity.Property(e => e.Serverity).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }
}
