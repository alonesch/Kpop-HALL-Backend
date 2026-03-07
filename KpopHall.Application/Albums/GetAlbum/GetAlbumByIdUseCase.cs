using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;


namespace KpopHall.Application.Albums.GetAlbum;

public class GetAlbumByIdUseCase
{
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;

    public GetAlbumByIdUseCase(IAlbumsRepository albumRepository, IArtistsRepository artistRepository)
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<GetAlbumByIdResponse> ExecuteAsync(Guid artistId, Guid albumId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null || album.ArtistId != artistId)
            throw new DomainException("Album not found.");

        return new GetAlbumByIdResponse
        {
            Id = album.Id,
            Title = album.Title,
            Year = album.Year,
            ArtistId = album.ArtistId
        };
    }
}
