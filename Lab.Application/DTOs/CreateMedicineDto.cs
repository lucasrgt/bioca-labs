using System.Text.Json.Serialization;
using Lab.Domain.Entities;

namespace Lab.Application.DTOs;

public record CreateMedicineInput(
    string Name,
    string CommercialName,
    string Description,
    string Color,
    string PatentNumber,
    string AnvisaNumber
);

public record CreateMedicineOutput(
    [property: JsonPropertyName("createdMedicine")]
    Medicine CreatedMedicine
);