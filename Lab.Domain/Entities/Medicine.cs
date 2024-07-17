using BiocaLabs.Common.Base;
using BiocaLabs.Common.Validation;
using Lab.Domain.Enums;

namespace Lab.Domain.Entities;

// 1: private set -> a classe não pode ser modificada externamente
// 2: sealed -> nao posso herdar dessa classe
// 3: validação -> segurança dos dados

public sealed class Medicine
    : BaseEntity
{
    public Medicine(string name, string commercialName, string description, MedicineColor color)
    {
        Validate(name, commercialName, description, color);
    }

    public string Name { get; private set; }
    public string CommercialName { get; private set; }
    public string Description { get; private set; }
    public MedicineColor Color { get; private set; }

    private void Validate(string name, string commercialName, string description, MedicineColor color)
    {
        StringValidator.Validate(name, "Name", 3, 100);
        Name = name;

        StringValidator.Validate(commercialName, "CommercialName", 5, 100);
        CommercialName = commercialName;

        StringValidator.Validate(description, "Description", 10, 255);
        Description = description;

        Color = color;
    }
}