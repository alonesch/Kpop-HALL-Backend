using KpopHall.Domain.Exceptions;


namespace KpopHall.Domain.Entities;

public class Photocard
{
    public Guid Id { get; private set; }
    public Guid AlbumId { get; private set; }
    public Guid ArtistId { get; private set; }
    public Guid MemberId { get; private set; }
    public Album Album { get; private set; } = null!;
    public Member Member { get; private set; } = null!;
    public Artist Artist { get; private set; } = null!;
    public string Version { get; private set; } =null!;
    public DistributionContext? DistributionContext { get; private set; }
    public string? FrontsideImagePath { get; private set; }
    public string? BacksideImagePath { get; private set; }
    protected Photocard() { }
    public Photocard(Guid id, Guid albumId,Guid artistId,Guid memberId, string version, DistributionContext? distributionContext = null)
    {
        if (string.IsNullOrWhiteSpace(version))
            throw new DomainException("Photocard name cannot be empty.");

        if (albumId <= Guid.Empty)
            throw new DomainException("Photocard must belong to a valid album.");
        Id = Guid.NewGuid();
        Version = version.Trim();
        AlbumId = albumId;
        ArtistId = artistId;
        MemberId = memberId;
        DistributionContext = distributionContext;
    }

    public bool IsIrregular => DistributionContext != null;

    public void SetImages(string? frontsidePath, string? backsidePath)
    {
         FrontsideImagePath = frontsidePath;
         BacksideImagePath = backsidePath;
    }
}
