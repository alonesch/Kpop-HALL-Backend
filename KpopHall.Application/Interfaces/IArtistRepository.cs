using KpopHall.Domain.Entities;

namespace KpopHall.Application.Interfaces;

public interface IArtistRepository
{
    Task AddAsync(Artist artist);
    Task<bool> ExistsByNameAsync (string name);
    Task<List<Artist>> GetAllAsync();
    Task<Artist?> GetByIdAsync (int id);
}
