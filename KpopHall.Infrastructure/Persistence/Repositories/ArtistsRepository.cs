using KpopHall.Domain.Entities;
using KpopHall.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KpopHall.Infrastructure.Persistence.Repositories;

public class ArtistsRepository : IArtistsRepository
{
    private readonly KpopHallDbContext _context;

    public ArtistsRepository(KpopHallDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Artist artist)
    {
        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Artists
            .AnyAsync(a => a.Name == name);
    }

    public async Task<List<Artist>> GetAllAsync()
    {
        return await _context.Artists
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Artist?> GetByIdAsync(int id)
    {
        return await _context.Artists
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task UpdateAsync(Artist artist)
    {
        _context.Artists.Update(artist);

        await _context.SaveChangesAsync();
    }
}