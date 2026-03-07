using KpopHall.Domain.Entities;
using KpopHall.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KpopHall.Infrastructure.Persistence.Repositories;

public class AlbumsRepository : IAlbumsRepository
{
    private readonly KpopHallDbContext _context;

    public AlbumsRepository(KpopHallDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Album album)
    {
        await _context.Albums.AddAsync(album);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsByTitleAndArtistIdAsync (string title, Guid artistId)
    {
        return await _context.Albums
            .AnyAsync(a => a.Title == title && a.ArtistId == artistId);
    }

    public async Task<List<Album>> GetByArtistIdAsync(Guid artistId)
    {
        return await _context.Albums
            .Where(a => a.ArtistId == artistId)
            .ToListAsync();
    }

    public async Task<Album?> GetByIdAsync(Guid id)
    {
        return await _context.Albums
            .FirstOrDefaultAsync(a => a.Id == id);
    }


}