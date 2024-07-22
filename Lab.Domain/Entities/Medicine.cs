using BiocaLabs.Common.Base;
using BiocaLabs.Common.Validation;
using Lab.Domain.Enums;
using Lab.Domain.VOs;

namespace Lab.Domain.Entities;

public sealed class Medicine
    : BaseEntity
{
    public Medicine()
    {
    }

    public Medicine(string name, string commercialName, string description,
        MedicineColor color,
        string patentNumber,
        string anvisaNumber, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Validate(name, commercialName, description);
        Name = name;
        CommercialName = commercialName;
        Description = description;
        Color = color;

        var medicineRegistration = new MedicineRegistration(patentNumber, anvisaNumber);
        Registration = medicineRegistration;
    }

    public string Name { get; private set; }
    public string CommercialName { get; private set; }
    public string Description { get; private set; }
    public MedicineColor Color { get; private set; }
    public MedicineRegistration Registration { get; private set; }

    private static void Validate(string name, string commercialName, string description)
    {
        StringValidator.Validate(name, "Name", 3, 100);
        StringValidator.Validate(commercialName, "CommercialName", 5, 100);
        StringValidator.Validate(description, "Description", 10, 255);
    }
}