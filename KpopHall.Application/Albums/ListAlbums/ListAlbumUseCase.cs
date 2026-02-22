using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Albums.ListAlbums;

public class ListAlbumsUseCase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;

    public ListAlbumsUseCase(IAlbumRepository albumRepository, IArtistRepository artistRepository)
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
