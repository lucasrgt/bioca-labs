using Lab.Application.DTOs;
using Lab.Domain.Repositories;

namespace Lab.Application.UseCases;

public sealed class GetMedicineByName(IMedicineRepository repository)
{
    public async Task<GetMedicineByNameOutput?> ExecuteAsync(string name)
    {
        // Call Repository
        var foundMedicine = await repository.FindOneMedicineByName(name);

        return foundMedicine is not null ? new GetMedicineByNameOutput(foundMedicine) : null;
    }
}