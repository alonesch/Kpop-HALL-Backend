using KpopHall.Domain.Exceptions;

namespace KpopHall.Domain.Entities;

public class Album
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public int Year { get; private set; }
    public Guid ArtistId { get; private set; }
    protected Album() { }
    public Album(string title, int year, Guid artistId)
    {
        ValidateTitle(title);
        ValidateYear(year);
        ValidateArtistId(artistId);

        Title = title.Trim();
        Year = year;
        ArtistId = artistId;
    }

    private void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Album title cannot be empty.");

        if (title.Length < 2)
            throw new DomainException("Album title must have at least 2 characters.");

        if (title.Length > 200)
            throw new DomainException("Album title cannot be exceed 200 characters.");
    }

    private void ValidateYear(int year)
    {
        if (year <= 0)
            throw new DomainException("Year must be greater than zero.");
    }

    private void ValidateArtistId(Guid artistid)
    {
        if (artistid <= Guid.Empty)
            throw new DomainException("ArtistId must be greater than zero");
    }
}
