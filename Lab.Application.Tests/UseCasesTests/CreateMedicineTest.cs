using BiocaLabs.Common.Exceptions;
using FluentAssertions;
using Lab.Application.DTOs;
using Lab.Application.UseCases;
using Lab.Domain.Entities;
using Lab.Domain.Enums;
using Lab.Domain.Repositories;
using Moq;

namespace Lab.Application.Tests.UseCasesTests;

public class CreateMedicineTest
{
    private const string ValidName = "MedicineName";
    private const string ValidCommercialName = "CommercialName";
    private const string ValidDescription = "Description";
    private const string ValidColorString = "Red";
    private const string ValidPatentNumber = "Patent123";
    private const string ValidAnvisaNumber = "123@";

    [Theory]
    [InlineData("Red", MedicineColor.Red)]
    public async Task ExecuteAsync_ShouldCreateMedicine_WhenColorIsValid(string colorString,
        MedicineColor expectedColor)
    {
        // Arrange
        var repositoryMock = new Mock<IMedicineRepository>();
        var createMedicine = new CreateMedicine(repositoryMock.Object);
        var input = new CreateMedicineInput(
            ValidName,
            ValidCommercialName,
            ValidDescription,
            colorString,
            ValidPatentNumber,
            ValidAnvisaNumber
        );

        // Act
        var result = await createMedicine.ExecuteAsync(input);

        // Assert
        result.CreatedMedicine.Color.Should().Be(expectedColor);
        result.CreatedMedicine.Name.Should().Be(input.Name);
        result.CreatedMedicine.CommercialName.Should().Be(input.CommercialName);
        result.CreatedMedicine.Description.Should().Be(input.Description);
        result.CreatedMedicine.Registration.PatentNumber.Should().Be(input.PatentNumber);
        result.CreatedMedicine.Registration.AnvisaNumber.Should().Be(input.AnvisaNumber);

        repositoryMock.Verify(r => r.SaveMedicineAsync(It.IsAny<Medicine>()), Times.Once);
    }

    [Theory]
    [InlineData("InvalidColor")]
    public async Task ExecuteAsync_ShouldThrowArgumentException_WhenColorIsInvalid(string colorString)
    {
        // Arrange
        var repositoryMock = new Mock<IMedicineRepository>();
        var createMedicine = new CreateMedicine(repositoryMock.Object);
        var input = new CreateMedicineInput(
            ValidName,
            ValidCommercialName,
            ValidDescription,
            colorString,
            ValidPatentNumber,
            ValidAnvisaNumber
        );

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => createMedicine.ExecuteAsync(input));
    }

    [Theory]
    [InlineData("")]
    public async Task ExecuteAsync_ShouldThrowStringValidationException_WhenNameIsInvalid(string name)
    {
        // Arrange
        var repositoryMock = new Mock<IMedicineRepository>();
        var createMedicine = new CreateMedicine(repositoryMock.Object);
        var input = new CreateMedicineInput(
            name,
            ValidCommercialName,
            ValidDescription,
            ValidColorString,
            ValidPatentNumber,
            ValidAnvisaNumber
        );

        // Act & Assert
        await Assert.ThrowsAsync<StringValidationException>(() => createMedicine.ExecuteAsync(input));
    }

    [Theory]
    [InlineData("")]
    public async Task ExecuteAsync_ShouldThrowStringValidationException_WhenAnvisaNumberIsInvalid(string anvisaNumber)
    {
        // Arrange
        var repositoryMock = new Mock<IMedicineRepository>();
        var createMedicine = new CreateMedicine(repositoryMock.Object);
        var input = new CreateMedicineInput(
            ValidName,
            ValidCommercialName,
            ValidDescription,
            ValidColorString,
            ValidPatentNumber,
            anvisaNumber
        );

        // Act & Assert
        await Assert.ThrowsAsync<StringValidationException>(() => createMedicine.ExecuteAsync(input));
    }
}