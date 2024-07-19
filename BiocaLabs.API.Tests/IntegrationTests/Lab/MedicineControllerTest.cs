using System.Text;
using System.Text.Json;
using FluentAssertions;
using Lab.Application.DTOs;

namespace BiocaLabs.IntegrationTests.IntegrationTests.Lab;

public class MedicineControllerTest : IntegrationTestBase
{
    [Fact]
    public async Task CreateMedicine_ReturnsCreatedMedicine()
    {
        // Arrange
        var client = Factory!.CreateClient();

        var input = new CreateMedicineInput(
            "Aspirin",
            "Aspirina",
            "Used for pain relief",
            "Red",
            "MJKP123",
            "123@"
        );
        var content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/Medicine", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        responseContent.Should().Contain("\"name\":\"Aspirin\"");
        responseContent.Should().Contain("\"commercialName\":\"Aspirina\"");
        responseContent.Should().Contain("\"description\":\"Used for pain relief\"");
        responseContent.Should().Contain("\"color\":2");
        responseContent.Should().Contain("\"patentNumber\":\"MJKP123\"");
        responseContent.Should().Contain("\"anvisaNumber\":\"123@\"");
    }
}