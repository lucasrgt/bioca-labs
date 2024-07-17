using Lab.Application.DTOs;
using Lab.Domain.Entities;
using Lab.Domain.Enums;
using Lab.Domain.Repositories;

namespace Lab.Application.UseCases;

// 1. acessar o dominio
// 2. acessar o repositorio
// 3. disparar eventos, enviar email, enviar um sms

public sealed class CreateMedicine(IMedicineRepository repository)
{
    public async Task<CreateMedicineOutput> ExecuteAsync(CreateMedicineInput input)
    {
        // Create Medicine Entity
        var color = (MedicineColor)Enum.Parse(typeof(MedicineColor), input.Color);
        var medicine = new Medicine(
            input.Name,
            input.CommercialName,
            input.Description,
            color,
            input.PatentNumber,
            input.AnvisaNumber
        );

        // Call Repository
        await repository.SaveMedicineAsync(medicine);

        return new CreateMedicineOutput(medicine);
    }
}