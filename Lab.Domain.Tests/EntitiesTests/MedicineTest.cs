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
    private const MedicineColor ValidColor = MedicineColor.Red;

    [Fact]
    public void Validate_ValidMedicine_DoesNotThrowException()
    {
        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, ValidDescription, ValidColor);

        // Assert
        createMedicine.Should().NotBeNull();
        createMedicine.Should().NotThrow();
    }

    [Fact]
    public void Validate_ShortName_ThrowsDomainValidationException()
    {
        // Arrange
        const string shortName = "a";

        // Act
        Action createMedicine = () => new Medicine(shortName, ValidCommercialName, ValidDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Validate_LongName_ThrowsDomainValidationException()
    {
        // Arrange
        var longName = new string('a', 101);

        // Act
        Action createMedicine = () => new Medicine(longName, ValidName, ValidDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Validate_ShortCommercialName_ThrowsDomainValidationException()
    {
        // Arrange
        const string shortCommercialName = "a";

        // Act
        Action createMedicine = () =>
            new Medicine(shortCommercialName, ValidCommercialName, ValidDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Validate_LongCommercialName_ThrowsDomainValidationException()
    {
        // Arrange
        var longCommercialName = new string('a', 101);

        // Act
        Action createMedicine = () => new Medicine(ValidName, longCommercialName, ValidDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Validate_ShortDescription_ThrowsDomainValidationException()
    {
        // Arrange
        const string shortDescription = "a";

        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, shortDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Validate_LongDescription_ThrowsDomainValidationException()
    {
        // Arrange
        var longDescription = new string('a', 256);

        // Act
        Action createMedicine = () => new Medicine(ValidName, ValidCommercialName, longDescription, ValidColor);

        // Assert
        createMedicine.Should().Throw<DomainValidationException>();
    }
}