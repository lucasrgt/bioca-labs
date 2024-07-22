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

    public async Task<Medicine> UpdateMedicineAsync(Guid id, Medicine newMedicine)
    {
        var existingMedicine = await FindOneMedicineById(id);
        if (existingMedicine is null) throw new InvalidOperationException();

        context.Entry(existingMedicine).CurrentValues.SetValues(newMedicine);
        context.Entry(existingMedicine).State = EntityState.Modified;
        context.Entry(existingMedicine).Property(o => o.CreatedOn).IsModified = false;
        await context.SaveChangesAsync();

        return existingMedicine;
    }

    public async Task DeleteMedicineAsync(Guid id)
    {
        var existingMedicine = await FindOneMedicineById(id);
        if (existingMedicine is null) throw new InvalidOperationException();

        context.Medicines.Remove(existingMedicine);
        await context.SaveChangesAsync();
    }


    public async Task<Medicine?> FindOneMedicineByName(string name)
    {
        return await context.Medicines.FirstOrDefaultAsync(medicine => medicine.Name == name);
    }

    public async Task<Medicine?> FindOneMedicineById(Guid id)
    {
        return await context.Medicines.FirstOrDefaultAsync(medicine => medicine.Id == id);
    }
}