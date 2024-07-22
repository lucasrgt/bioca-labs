using Lab.Application.DTOs;
using Lab.Domain.Entities;
using Lab.Domain.Enums;
using Lab.Domain.Repositories;

namespace Lab.Application.UseCases;

public sealed class UpdateMedicine(IMedicineRepository repository)
{
    public async Task<UpdateMedicineOutput> ExecuteAsync(Guid id, UpdateMedicineInput input)
    {
        // Create Medicine Entity
        var color = (MedicineColor)Enum.Parse(typeof(MedicineColor), input.Color);
        var medicine = new Medicine(
            input.Name,
            input.CommercialName,
            input.Description,
            color,
            input.PatentNumber,
            input.AnvisaNumber,
            id
        );

        // Call Repository
        var output = await repository.UpdateMedicineAsync(id, medicine);

        return new UpdateMedicineOutput(output);
    }
}