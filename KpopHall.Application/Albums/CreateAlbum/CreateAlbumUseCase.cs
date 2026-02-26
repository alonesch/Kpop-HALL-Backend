using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Albums.CreateAlbum;

public class CreateAlbumUseCase
{
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;

    public CreateAlbumUseCase(IAlbumsRepository albumRepository, IArtistsRepository artistRepository)
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<CreateAlbumResponse> ExecuteAsync (int artistId, CreateAlbumRequest request)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var albumExists = await _albumRepository.ExistsByTitleAndArtistIdAsync(request.Title, artistId);
        if (albumExists)
            throw new DomainException("An album with the same title already exists for this artist.");

        var album = new Album(request.Title, request.Year, artistId);
        await _albumRepository.AddAsync(album);
        return new CreateAlbumResponse
        {
            Id = album.Id,
            Title = album.Title,
            Year = album.Year,
            ArtistId = album.ArtistId,

        };


    }
}