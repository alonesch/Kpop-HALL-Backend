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

    public async Task<UpdateArtistResponse> ExecuteAsync(int id, UpdateArtistRequest request)
    {
        var artist = await repository.GetByIdAsync(id);
        if (artist == null)
            throw new DomainException("Artist not found.");


        Console.WriteLine($"Nome do artista no banco: {artist.Name}");
        Console.WriteLine ($"Novo nome do artista: {request.Name}");

        artist.Rename(request.Name);


        await repository.UpdateAsync(artist);

        Console.WriteLine($"Nome do artista após atualização: {artist.Name}");

        var updatedArtist = await repository.GetByIdAsync(id);
        Console.WriteLine($"Nome após o update: {updatedArtist.Name}");

        return new UpdateArtistResponse
        {
            Id = artist.Id,
            Name = artist.Name
        };
    }
}
