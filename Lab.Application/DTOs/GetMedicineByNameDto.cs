using System.Text.Json.Serialization;
using Lab.Domain.Entities;

namespace Lab.Application.DTOs;

public record GetMedicineByNameOutput(
    [property: JsonPropertyName("medicine")]
    Medicine Medicine
);