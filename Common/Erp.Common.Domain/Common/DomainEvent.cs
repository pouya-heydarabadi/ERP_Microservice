namespace Erp.Common.Domain.Common;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
} 