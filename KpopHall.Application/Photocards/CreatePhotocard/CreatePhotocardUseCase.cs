using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
using KpopHall.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KpopHall.Application.Photocards.CreatePhotocard;

public class CreatePhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;
    private readonly IMembersRepository _memberRepository;

    public CreatePhotocardUseCase(IPhotoCardsRepository photoCardRepository, IAlbumsRepository albumRepository, IArtistsRepository artistRepository, IMembersRepository memberRepository)
    {
        _photoCardRepository = photoCardRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _memberRepository = memberRepository;
    }
    public async Task<CreatePhotocardResponse> ExecuteAsync(Guid artistId, Guid albumId, CreatePhotocardRequest request)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");

        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null || album.ArtistId != artistId)
            throw new DomainException("Album not found.");

        var exists = await _photoCardRepository.ExistsByVersionAndAlbumIdAsync(request.Version, albumId);
        if (exists)
            throw new DomainException("A photocard with the same name already exists in this album.");

        var member = await _memberRepository.GetByIdAsync(request.MemberId);
        if (member == null || member.ArtistId != artistId)
            throw new DomainException("Member not found.");

        Photocard photocard;

        if (request.DistributionContext == null)
        {
            photocard = new Photocard(Guid.NewGuid(),albumId, artistId, request.MemberId, request.Version);
        }
        else
        {
            var context = new DistributionContext(
                request.DistributionContext.Store,
                request.DistributionContext.Region,
                request.DistributionContext.Event,
                request.DistributionContext.PrintQuantity
         );

            photocard = new Photocard(Guid.NewGuid(), albumId, artistId, request.MemberId, request.Version, context);
        }
        await _photoCardRepository.AddAsync(photocard);

        return new CreatePhotocardResponse
        {
            Id = photocard.Id,
            Version = photocard.Version,
            AlbumId = photocard.AlbumId,
            MemberId = photocard.MemberId,
            IsIrregular = photocard.IsIrregular
        };

    }
}
