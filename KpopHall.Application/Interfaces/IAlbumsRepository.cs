using KpopHall.Domain.Entities;

namespace KpopHall.Application.Interfaces;

public interface IAlbumsRepository
{
    Task AddAsync(Album album);
    Task<bool> ExistsByTitleAndArtistIdAsync(string title, int artistId);
    Task<List<Album>> GetByArtistIdAsync(int artistId);
    Task<Album?> GetByIdAsync(int id);

}