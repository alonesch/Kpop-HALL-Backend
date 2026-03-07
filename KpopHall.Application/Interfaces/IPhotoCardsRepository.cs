using KpopHall.Domain.Entities;
namespace KpopHall.Application.Interfaces;

public interface IPhotoCardsRepository
{
    Task AddAsync(Photocard photocard);
    Task<Photocard?> GetByIdAsync(Guid id);
    Task<List<Photocard>> GetAllAsync();
    Task<List<Photocard>> GetByAlbumIdAsync(Guid albumId);
    Task<List<Photocard>> GetByArtistIdAsync(Guid artistId);
    Task<List<Photocard>> GetByMemberIdAsync(Guid memberId);
    Task<bool> ExistsByVersionAndAlbumIdAsync(string Version, Guid albumId);
}