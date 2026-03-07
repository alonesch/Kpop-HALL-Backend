using KpopHall.Domain.Entities;

namespace KpopHall.Application.Interfaces;

public interface IAlbumsRepository
{
    Task AddAsync(Album album);
    Task<bool> ExistsByTitleAndArtistIdAsync(string title, Guid artistId);
    Task<List<Album>> GetByArtistIdAsync(Guid artistId);
    Task<Album?> GetByIdAsync(Guid id);

}