using KpopHall.Domain.Exceptions;

namespace KpopHall.Domain.Entities;

public class Artist
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    protected Artist() { }
    public Artist(string name)
    {
        SetName(name);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Artist name cannot be empty.");
        if (name.Length < 2)
            throw new DomainException("Artist name must be at least 2 characters long.");

        if (name.Length > 200)
            throw new DomainException("Artist name cannot exceed 200 characters.");

        Name = name.Trim();

    }
}
