using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Artists.GetArtistById;

public class GetArtistByIdUseCase
{
    private readonly IArtistRepository repository;

    public GetArtistByIdUseCase(IArtistRepository repository)
    {
        this.repository = repository;
    }

    public async Task<GetArtistByIdResponse> ExecuteAsync(int id)
    {
      var artist = await repository.GetByIdAsync(id);

        if (artist == null)
            throw new DomainException("Artist not found.");

        return new GetArtistByIdResponse
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }
}
