using KpopHall.Domain.Exceptions;


namespace KpopHall.Domain.Entities;

public class Photocard
{
    public Guid Id { get; private set; }
    public Guid AlbumId { get; private set; }
    public Guid MemberId { get; private set; }
    public string Version { get; private set; } =null!;
    public DistributionContext? DistributionContext { get; private set; }
    protected Photocard() { }
    public Photocard(Guid albumId,Guid memberId, string version, DistributionContext? distributionContext = null)
    {
        if (string.IsNullOrWhiteSpace(version))
            throw new DomainException("Photocard name cannot be empty.");

        if (albumId <= Guid.Empty)
            throw new DomainException("Photocard must belong to a valid album.");

        Version = version.Trim();
        AlbumId = albumId;
        MemberId = memberId;
        DistributionContext = distributionContext;
    }

    public bool IsIrregular => DistributionContext != null;


}
