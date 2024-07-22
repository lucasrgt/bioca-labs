using Lab.Domain.Entities;

namespace Lab.Domain.Repositories;

public interface IMedicineRepository
{
    Task<Medicine?> FindOneMedicineByName(string name);
    Task<Medicine?> FindOneMedicineById(Guid id);
    Task SaveMedicineAsync(Medicine medicine);
    Task<Medicine> UpdateMedicineAsync(Guid id, Medicine medicine);
    Task DeleteMedicineAsync(Guid id);
}