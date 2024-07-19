using BiocaLabs.Data.DbContext;
using Lab.Domain.Entities;
using Lab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab.Infrastructure.RepositoriesImpl;

public class MedicineRepositoryImpl(AppDbContext context) : IMedicineRepository
{
    public async Task SaveMedicineAsync(Medicine medicine)
    {
        await context.Medicines.AddAsync(medicine);
        await context.SaveChangesAsync();
    }

    public async Task<Medicine?> FindOneMedicineByName(string name)
    {
        return await context.Medicines.FirstOrDefaultAsync(medicine => medicine.Name == name);
    }
}