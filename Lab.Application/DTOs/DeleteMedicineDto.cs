using System.Text.Json.Serialization;

namespace Lab.Application.DTOs;

public record DeleteMedicineOutput(
    [property: JsonPropertyName("id")] Guid id
);