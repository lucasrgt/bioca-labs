using Lab.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiocaLabs.Data.DbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    // Lab
    public DbSet<Medicine> Medicines { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedDefaultUser.RegisterDefaultUser(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Medicine>().OwnsOne(m => m.Registration);
    }
}