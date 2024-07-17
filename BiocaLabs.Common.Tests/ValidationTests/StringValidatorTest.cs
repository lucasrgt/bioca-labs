using BiocaLabs.Common.Exceptions;
using BiocaLabs.Common.Validation;
using FluentAssertions;

namespace BiocaLabs.Common.Tests.ValidationTests;

public class StringValidatorTest
{
    [Fact]
    public void Validate_ValidString_DoesNotThrowException()
    {
        // Arrange
        const string validString = "valid string";

        // Act
        var action = () => StringValidator.Validate(validString, "TestString", 12, 12);

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void Validate_EmptyString_ThrowsStringValidationException()
    {
        // Arrange
        const string emptyString = "";

        // Act
        var action = () => StringValidator.Validate(emptyString, "TestString", 8, 12);

        // Assert
        action.Should().Throw<StringValidationException>()
            .WithMessage("Invalid TestString. TestString is required.");
    }

    [Fact]
    public void Validate_WhiteSpaceString_ThrowsStringValidationException()
    {
        // Arrange
        const string whiteSpaceString = " ";

        // Act
        var action = () => StringValidator.Validate(whiteSpaceString, "TestString", 8, 12);

        // Assert
        action.Should().Throw<StringValidationException>()
            .WithMessage("Invalid TestString. TestString is required.");
    }

    [Fact]
    public void Validate_PreWhiteSpaceString_ThrowsStringValidationException()
    {
        // Arrange
        const string preWhiteSpaceString = " name";

        // Act
        var action = () => StringValidator.Validate(preWhiteSpaceString, "TestString", 1, 12);

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void Validate_ShortString_ThrowsStringValidationException()
    {
        // Arrange
        const string shortString = "invalid";

        // Act
        var action = () => StringValidator.Validate(shortString, "TestString", 8, 12);

        // Assert
        action.Should().Throw<StringValidationException>()
            .WithMessage("Invalid TestString. TestString must have at least 8 characters.");
    }

    [Fact]
    public void Validate_LongString_ThrowsStringValidationException()
    {
        // Arrange
        var longString = new string('a', 13);

        // Act
        var action = () => StringValidator.Validate(longString, "TestString", 8, 12);

        // Assert
        action.Should().Throw<StringValidationException>()
            .WithMessage("Invalid TestString. TestString must have 12 characters or less.");
    }
}