using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Photocards.ListPhotocard;

public class  ListPhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;


    public ListPhotocardUseCase(IPhotoCardsRepository photoCardRepository, IAlbumsRepository albumRepository, IArtistsRepository artistRepository)
    {
        _photoCardRepository = photoCardRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<List<ListPhotocardResponse>>ExecuteAsync(int artistId, int albumId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null || album.ArtistId != artistId)
            throw new DomainException("Album not found.");

        var photocards = await _photoCardRepository.GetByAlbumIdAsync(albumId);

        return photocards.Select(p => new ListPhotocardResponse
        {
            Id = p.Id,
            Name = p.Name,
            IsIrregular = p.IsIrregular
        }).ToList();
    }
}