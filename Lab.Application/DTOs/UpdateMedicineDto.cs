using System.Text.Json.Serialization;
using Lab.Domain.Entities;

namespace Lab.Application.DTOs;

public record UpdateMedicineInput(
    string Name,
    string CommercialName,
    string Description,
    string Color,
    string PatentNumber,
    string AnvisaNumber
);
 
public record UpdateMedicineOutput(
    [property: JsonPropertyName("updatedMedicine")]
    Medicine UpdatedMedicine
);