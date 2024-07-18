using Lab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    // Lab
    public DbSet<Medicine> Medicines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Medicine>().OwnsOne(m => m.Registration);
    }
}