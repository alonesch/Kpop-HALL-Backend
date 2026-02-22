using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Photocards.ListPhotocard;

public class  ListPhotocardUseCase
{
    private readonly IPhotoCardRepository _photoCardRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;


    public ListPhotocardUseCase(IPhotoCardRepository photoCardRepository, IAlbumRepository albumRepository, IArtistRepository artistRepository)
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