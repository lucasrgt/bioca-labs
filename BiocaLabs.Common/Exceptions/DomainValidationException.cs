namespace BiocaLabs.Common.Validation;

public class DomainValidationException(string message) : Exception(message)
{
    public static void When(bool isValid, string message)
    {
        if (!isValid)
        {
            throw new DomainValidationException(message);
        }
    }
}

