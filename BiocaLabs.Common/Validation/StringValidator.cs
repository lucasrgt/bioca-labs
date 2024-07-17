using BiocaLabs.Common.Exceptions;

namespace BiocaLabs.Common.Validation;

public static class StringValidator
{
    public static void Validate(string value, string valueName, int minLength, int maxLength)
    {
        var lowerValueName = valueName.ToLower();
        DomainValidationException.When(string.IsNullOrEmpty(value),
            $"Invalid {lowerValueName}. {valueName} is required.");
        DomainValidationException.When(string.IsNullOrWhiteSpace(value),
            $"Invalid {lowerValueName}. {valueName} is required.");
        DomainValidationException.When(value.Length < minLength,
            $"Invalid {lowerValueName}. {valueName} must have at least {minLength} characters.");
        DomainValidationException.When(value.Length > maxLength,
            $"Invalid {lowerValueName}. {valueName} must have {maxLength} characters or less.");
    }
}