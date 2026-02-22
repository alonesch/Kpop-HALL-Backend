using KpopHall.Domain.Entities;

namespace KpopHall.Application.Interfaces;

public interface IPhotoCardRepository
{
    Task AddAsync(Photocard photocard);
    Task<List<Photocard>> GetByAlbumIdAsync(int albumId);
    Task<Photocard?> GetByIdAsync(int id);
    Task<bool> ExistsByNameAndAlbumIdAsync(string name, int albumId);
}
