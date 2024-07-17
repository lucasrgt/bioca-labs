namespace BiocaLabs.Common.Base;

public class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; private set; } = DateTime.Now;
    public DateTime UpdatedOn { get; private set; } = DateTime.Now;

    public BaseEntity()
    {
        
    }
}