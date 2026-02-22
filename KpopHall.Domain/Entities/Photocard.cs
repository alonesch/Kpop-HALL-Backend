using KpopHall.Domain.Exceptions;


namespace KpopHall.Domain.Entities;

public class Photocard
{
    public int Id { get; private set; }
    public int AlbumId { get; private set; }
    public string Name { get; private set; } =null!;
    public DistributionContext? DistributionContext { get; private set; }
    protected Photocard() { }
    public Photocard(int albumId, string name, DistributionContext? distributionContext = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Photocard name cannot be empty.");

        if (albumId <= 0)
            throw new DomainException("Photocard must belong to a valid album.");

        Name = name.Trim();
        AlbumId = albumId;
        DistributionContext = distributionContext;
    }

    public bool IsIrregular => DistributionContext != null;


}
