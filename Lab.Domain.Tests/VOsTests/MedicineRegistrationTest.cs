using BiocaLabs.Common.Exceptions;
using FluentAssertions;
using Lab.Domain.VOs;

namespace Lab.Domain.Tests.VOsTests;

public class MedicineRegistrationTest
{
    private const string ValidPatentNumber = "MJKP123";
    private const string ValidAnvisaNumber = "123@";

    [Fact]
    public void Validate_ValidMedicineRegistration_DoesNotThrowException()
    {
        // Act
        Action createMedicineRegistration = () => new MedicineRegistration(ValidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicineRegistration.Should().NotBeNull();
        createMedicineRegistration.Should().NotThrow();
    }

    [Fact]
    public void Validate_InvalidPatentNumber_ThrowsDomainValidationException()
    {
        // Arrange
        const string invalidPatentNumber = "a";

        // Act
        Action createMedicineRegistration = () => new MedicineRegistration(invalidPatentNumber, ValidAnvisaNumber);

        // Assert
        createMedicineRegistration.Should().Throw<DomainValidationException>()
            .WithMessage("PatentNumber must contain at least one letter and one number.");
    }

    [Fact]
    public void Validate_InvalidAnvisaNumber_ThrowsDomainValidationException()
    {
        // Arrange
        const string invalidAnvisaNumber = "a";

        // Act
        Action createMedicineRegistration = () => new MedicineRegistration(ValidPatentNumber, invalidAnvisaNumber);

        // Assert
        createMedicineRegistration.Should().Throw<DomainValidationException>()
            .WithMessage("AnvisaNumber must contain only numbers followed by an @.");
    }
}