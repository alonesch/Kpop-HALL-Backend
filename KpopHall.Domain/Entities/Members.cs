using KpopHall.Domain.Exceptions;


namespace KpopHall.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid ArtistId { get; private set; }

    protected Member() { }

    public Member(Guid id,string name, Guid artistId)
    {
        if (id == Guid.Empty)
            throw new DomainException("Member id is invalid.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Member name cannot be null.");

        if (artistId == Guid.Empty)
            throw new DomainException("Artist id is invalid.");
        {
               Id = id;
            Name = name.Trim();
            ArtistId = artistId;
        }
    }
}
