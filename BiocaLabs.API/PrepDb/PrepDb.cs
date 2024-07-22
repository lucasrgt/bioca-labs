using BiocaLabs.Data.DbContext;
using Lab.Domain.Entities;
using Lab.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        using var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!db.Database.GetPendingMigrations().Any()) return;

        db.Database.Migrate();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Medicines.Any())
        {
            Console.WriteLine("--> Seeding Data...");
            context.Medicines.AddRange(
                new Medicine("Tylenol", "Paracetamol", "Um remédio forte para febre", MedicineColor.Red, "MJKP123",
                    "123@"),
                new Medicine("Dipirona", "Dipironex", "Um remédio muito bom para dor", MedicineColor.Generic,
                    "BIOCA2469", "692@"),
                new Medicine("Salbutamol", "Aerolin", "O melhor remédio para asma", MedicineColor.Exempt, "TRINCA753",
                    "234@")
            );

            context.SaveChanges();
            return;
        }

        Console.WriteLine("--> We already have data");
    }
}