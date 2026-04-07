using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace KpopHall.Application.Photocards.ListPhotocard;

public class ListPhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    private readonly IAlbumsRepository _albumRepository;
    private readonly IArtistsRepository _artistRepository;
    private readonly string _cdnBaseUrl;
    public ListPhotocardUseCase(
        IPhotoCardsRepository photoCardRepository,
        IAlbumsRepository albumRepository,
        IArtistsRepository artistRepository,
        IConfiguration configuration)
    {
        _photoCardRepository = photoCardRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _cdnBaseUrl = configuration["Cdn:BaseUrl"] ?? string.Empty;
    }
    public async Task<List<ListPhotocardResponse>> ExecuteAsync()
    {
        var photocards = await _photoCardRepository.GetAllAsync();
        return photocards.Select(p => Map(p, _cdnBaseUrl)).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByAlbumAsync(Guid albumId)
    {
        var album = await _albumRepository.GetByIdAsync(albumId);
        if (album == null)
            throw new DomainException("Album not found.");
        var photocards = await _photoCardRepository.GetByAlbumIdAsync(albumId);
        return photocards.Select(p => Map(p, _cdnBaseUrl)).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByArtistAsync(Guid artistId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");
        var photocards = await _photoCardRepository.GetByArtistIdAsync(artistId);
        return photocards.Select(p => Map(p, _cdnBaseUrl)).ToList();
    }
    public async Task<List<ListPhotocardResponse>> ExecuteByMemberAsync(Guid memberId)
    {
        var photocards = await _photoCardRepository.GetByMemberIdAsync(memberId);
        if (photocards == null || photocards.Count == 0)
            throw new DomainException("No photocards found for this member.");
        return photocards.Select(p => Map(p, _cdnBaseUrl)).ToList();
    }
    private static ListPhotocardResponse Map(Photocard p, string cdnBaseUrl) => new()
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
        ? $"{cdnBaseUrl}/{p.FrontsideImagePath}"
        : null
    };
}