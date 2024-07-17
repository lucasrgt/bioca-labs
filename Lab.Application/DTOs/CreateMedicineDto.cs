using Lab.Domain.Entities;

namespace Lab.Application.DTOs;

public record CreateMedicineInput(
    string Name,
    string CommercialName,
    string Description,
    string Color,
    string PatentNumber,
    string AnvisaNumber);

public record CreateMedicineOutput(
    Medicine CreatedMedicine
);