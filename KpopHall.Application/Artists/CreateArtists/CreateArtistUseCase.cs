using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
using KpopHall.Domain.Entities;

namespace KpopHall.Application.Artists.CreateArtist;

public class CreateArtistUseCase
{
    private readonly IArtistsRepository _repository;

    public CreateArtistUseCase(IArtistsRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateArtistResponse> ExecuteAsync(CreateArtistRequest request)
    {
        var exists = await _repository.ExistsByNameAsync(request.Name);

        if (exists)
            throw new DomainException("Artist with this name already exists.");

        var artist = new Artist(request.Name);

        await _repository.AddAsync(artist);

        return new CreateArtistResponse
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }
}