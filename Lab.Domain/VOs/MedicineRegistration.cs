using System.Text.RegularExpressions;
using BiocaLabs.Common.Exceptions;
using BiocaLabs.Common.Validation;

namespace Lab.Domain.VOs;

public sealed partial class MedicineRegistration
{
    public MedicineRegistration(string patentNumber, string anvisaNumber)
    {
        Validate(patentNumber, anvisaNumber);
        PatentNumber = patentNumber;
        AnvisaNumber = anvisaNumber;
    }

    public string PatentNumber { get; private set; }
    public string AnvisaNumber { get; private set; }

    private static void Validate(string patentNumber, string anvisaNumber)
    {
        var patentNumberRegex = PatentNumberRegex();
        var anvisaNumberRegex = AnvisaNumberRegex();

        StringValidator.Validate(patentNumber, "PatentNumber", 1, 12);
        StringValidator.Validate(anvisaNumber, "AnvisaNumber", 1, 12);

        if (!patentNumberRegex.IsMatch(patentNumber))
            throw new DomainValidationException("PatentNumber must contain at least one letter and one number.");

        if (!anvisaNumberRegex.IsMatch(anvisaNumber))
            throw new DomainValidationException("AnvisaNumber must contain only numbers followed by an @.");
    }

    [GeneratedRegex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$", RegexOptions.Compiled)]
    private static partial Regex PatentNumberRegex();

    [GeneratedRegex(@"^\d+@$", RegexOptions.Compiled)]
    private static partial Regex AnvisaNumberRegex();
}