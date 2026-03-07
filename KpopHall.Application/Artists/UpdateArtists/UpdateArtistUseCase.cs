using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Artists.UpdateArtists;

public class UpdateArtistUseCase
{
    private readonly IArtistsRepository repository;

    public UpdateArtistUseCase(IArtistsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateArtistResponse> ExecuteAsync(Guid id, UpdateArtistRequest request)
    {
        var artist = await repository.GetByIdAsync(id);
        if (artist == null)
            throw new DomainException("Artist not found.");

        artist.Rename(request.Name);

        await repository.UpdateAsync(artist);

        var updatedArtist = await repository.GetByIdAsync(id);

        return new UpdateArtistResponse
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }
}
