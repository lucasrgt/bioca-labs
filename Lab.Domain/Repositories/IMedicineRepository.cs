using Lab.Domain.Entities;

namespace Lab.Domain.Repositories;

public interface IMedicineRepository
{
    Task SaveMedicineAsync(Medicine medicine);
    Task<Medicine?> FindOneMedicineByName(string name);
}