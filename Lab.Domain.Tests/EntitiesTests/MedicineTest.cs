using BiocaLabs.Common.Exceptions;
using FluentAssertions;
using Lab.Domain.Entities;
using Lab.Domain.Enums;

namespace Lab.Domain.Tests.EntitiesTests;

public class MedicineTest
{
    private const string ValidName = "Paracetamol";
    private const string ValidCommercialName = "Tylenol";
    private const string ValidDescription = "Um remédio forte para febre.";
    private const string ValidPatentNumber = "MJKP123";
    private const string ValidAnvisaNumber = "123@";
    private const MedicineColor ValidColor = MedicineColor.Red;

    [Fact]
    public void Validate_ValidMedicine_DoesNotThrowException()
    {
        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, ValidDescription, ValidColor,
            ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().NotBeNull();
        createMedicine.Should().NotThrow();
    }

    [Fact]
    public void Validate_ShortName_ThrowsStringValidationException()
    {
        // Arrange
        const string shortName = "a";

        // Act
        Action createMedicine = () => new Medicine(shortName, ValidCommercialName, ValidDescription, ValidColor,
            ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_LongName_ThrowsStringValidationException()
    {
        // Arrange
        var longName = new string('a', 101);

        // Act
        Action createMedicine = () =>
            new Medicine(longName, ValidName, ValidDescription, ValidColor, ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_ShortCommercialName_ThrowsStringValidationException()
    {
        // Arrange
        const string shortCommercialName = "a";

        // Act
        Action createMedicine = () =>
            new Medicine(shortCommercialName, ValidCommercialName, ValidDescription, ValidColor, ValidPatentNumber,
                ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_LongCommercialName_ThrowsStringValidationException()
    {
        // Arrange
        var longCommercialName = new string('a', 101);

        // Act
        Action createMedicine = () => new Medicine(ValidName, longCommercialName, ValidDescription, ValidColor,
            ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_ShortDescription_ThrowsStringValidationException()
    {
        // Arrange
        const string shortDescription = "a";

        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, shortDescription, ValidColor,
            ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_LongDescription_ThrowsStringValidationException()
    {
        // Arrange
        var longDescription = new string('a', 256);

        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, longDescription, ValidColor,
            ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicine.Should().Throw<StringValidationException>();
    }

    [Fact]
    public void Validate_InvalidMedicineRegistrationData_ThrowsDomainValidationException()
    {
        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, ValidDescription, ValidColor,
            "a", "a");

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }
}