using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Photocards.GetPhotocard;

public class GetPhotocardUseCase
{
    private readonly IPhotoCardRepository _photoCardRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;

    public GetPhotocardUseCase(IPhotoCardRepository photoCardRepository, IAlbumRepository albumRepository, IArtistRepository artistRepository)
    {
        _photoCardRepository = photoCardRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }

    public async Task<GetPhotocardResponse> ExecuteAsync( int artistId, int albumId, int photocardId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null || album.ArtistId != artistId)
            throw new DomainException("Album not found.");

        var photocard = await _photoCardRepository.GetByIdAsync(photocardId);
        if (photocard == null || photocard.AlbumId != albumId)
            throw new DomainException("Photocard not found.");

        return new GetPhotocardResponse
        {
            Id = photocard.Id,
            Name = photocard.Name,
            AlbumId = photocard.AlbumId,
            IsIrregular = photocard.IsIrregular,
            Store = photocard.DistributionContext?.Store,
            Region = photocard.DistributionContext?.Region,
            Event = photocard.DistributionContext?.Event,
            PrintQuantity = photocard.DistributionContext?.PrintQuantity
        };
    }
}
