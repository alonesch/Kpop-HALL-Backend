using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Photocards.ListPhotocard;

public class ListPhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;
    public ListPhotocardUseCase(
        IPhotoCardsRepository photoCardRepository,
        IAlbumsRepository albumRepository,
        IArtistsRepository artistRepository)
    {
        _photoCardRepository = photoCardRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
    }
    public async Task<List<ListPhotocardResponse>> ExecuteAsync()
    {
        var photocards = await _photoCardRepository.GetAllAsync();
        return photocards.Select(Map).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByAlbumAsync(Guid albumId)
    {
        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null)
            throw new DomainException("Album not found.");
        var photocards = await _photoCardRepository.GetByAlbumIdAsync(albumId);
        return photocards.Select(Map).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByArtistAsync(Guid artistId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");
        var photocards = await _photoCardRepository.GetByArtistIdAsync(artistId);
        return photocards.Select(Map).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByMemberAsync(Guid memberId)
    {
        var photocards = await _photoCardRepository.GetByMemberIdAsync(memberId);
        if (photocards == null || photocards.Count == 0)
            throw new DomainException("No photocards found for this member.");
        return photocards.Select(Map).ToList();
    }
    private static ListPhotocardResponse Map(Photocard p) => new()
    {
        Id = p.Id,
        Version = p.Version,
        MemberId = p.MemberId,
        MemberName = p.Member.Name,
        ArtistId = p.ArtistId,
        ArtistName = p.Album.Artist.Name,
        AlbumTitle = p.Album.Title,
        IsIrregular = p.IsIrregular,
        FrontsideImageUrl = p.FrontsideImagePath != null
         ? $"https://cdn.kpophall.com/{p.FrontsideImagePath}"
         : null
    };
}