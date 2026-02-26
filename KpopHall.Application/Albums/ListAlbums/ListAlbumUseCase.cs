using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Albums.ListAlbums;

public class ListAlbumsUseCase
{
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;

    public ListAlbumsUseCase(IAlbumsRepository albumRepository, IArtistsRepository artistRepository)
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<List<ListAlbumsResponse>> ExecuteAsync(int artistId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var albums = await _albumRepository.GetByArtistIdAsync(artistId);

        return albums.Select(a => new ListAlbumsResponse
        {
            Id = a.Id,
            Title = a.Title,
            Year = a.Year
        }).ToList();
    }
}
