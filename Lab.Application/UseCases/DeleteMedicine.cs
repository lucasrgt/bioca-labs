using Lab.Application.DTOs;
using Lab.Domain.Repositories;

namespace Lab.Application.UseCases;

public sealed class DeleteMedicine(IMedicineRepository repository)
{
    public async Task<DeleteMedicineOutput> ExecuteAsync(Guid id)
    {
        // Call Repository
        await repository.DeleteMedicineAsync(id);

        return new DeleteMedicineOutput(id);
    }
}