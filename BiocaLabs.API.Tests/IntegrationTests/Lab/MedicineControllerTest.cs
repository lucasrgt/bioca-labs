using System.Net;
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

    [Fact]
    public async Task GetMedicineByName_ReturnsMedicine_WhenExists()
    {
        // Arrange
        var client = Factory!.CreateClient();

        var createInput = new CreateMedicineInput(
            "Aspirin",
            "Aspirina",
            "Used for pain relief",
            "Red",
            "MJKP123",
            "123@"
        );
        var createContent = new StringContent(JsonSerializer.Serialize(createInput), Encoding.UTF8, "application/json");

        // Create a medicine to test retrieving it
        await client.PostAsync("/api/Medicine", createContent);

        // Act
        var response = await client.GetAsync("/api/Medicine/Aspirin");
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

    [Fact]
    public async Task GetMedicineByName_ReturnsNotFound_WhenDoesNotExist()
    {
        // Arrange
        var client = Factory!.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Medicine/NonExistentMedicine");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var responseContent = await response.Content.ReadAsStringAsync();
        responseContent.Should().Contain("Medicine with name 'NonExistentMedicine' not found.");
    }
}