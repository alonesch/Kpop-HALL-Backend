using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace KpopHall.Infrastructure.Persistence.Repositories;

public class PhotocardsRepository : IPhotoCardsRepository
{
    private readonly KpopHallDbContext _context;
    public PhotocardsRepository(KpopHallDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Photocard photocard)
    {
        await _context.Photocards.AddAsync(photocard);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Photocard>> GetByAlbumIdAsync(int albumId)
    {
        return await _context.Photocards
            .AsNoTracking()
            .Where(p => p.AlbumId == albumId)
            .ToListAsync();
    }

    public async Task<Photocard?> GetByIdAsync(int id)
    {
        return await _context.Photocards
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByNameAndAlbumIdAsync(string name, int albumId)
    {
        return await _context.Photocards
            .AnyAsync(p => p.Name == name && p.AlbumId == albumId);
    }


}
