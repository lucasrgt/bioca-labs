using Lab.Domain.Entities;
using Lab.Domain.Repositories;
using Lab.Infrastructure.DbContext;

namespace Lab.Infrastructure.RepositoriesImpl;

public class MedicineRepositoryImpl(AppDbContext context) : IMedicineRepository
{
    public async Task SaveMedicineAsync(Medicine medicine)
    {
        await context.Medicines.AddAsync(medicine);
        await context.SaveChangesAsync();
    }
}