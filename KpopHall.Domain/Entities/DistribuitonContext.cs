using KpopHall.Domain.Exceptions;


namespace KpopHall.Domain.Entities;

public class DistributionContext
{
    public string Store { get; private set; } = null!;
    public string? Region { get; private set; }
    public string? Event { get; private set; }
    public int? PrintQuantity { get; private set; }

    protected DistributionContext() { }

    public DistributionContext(string store, string? region = null, string? @event = null, int? printQuantity = null)
    {
        if (string.IsNullOrWhiteSpace(store))
            throw new DomainException("Irregular photocards must have a store.");

        if (printQuantity.HasValue && printQuantity <= 0)
            throw new DomainException("Print quantity must be greater than zero.");

        Store = store.Trim();
        Region = region?.Trim();
        Event = @event?.Trim();
        PrintQuantity = printQuantity;
    }
}
