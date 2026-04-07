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

    public async Task<List<Photocard>> GetByAlbumIdAsync(Guid albumId)
    {
        return await _context.Photocards
            .AsNoTracking()
            .Where(p => p.AlbumId == albumId)
            .ToListAsync();
    }

    public async Task<List<Photocard>> GetAllAsync()
    {
        return await _context.Photocards
            .AsNoTracking()
            .Include(p => p.Album)
                .ThenInclude(a => a.Artist)
             .Include(p => p.Member)
            .ToListAsync();
    }

    public async Task<Photocard?> GetByIdAsync(Guid id)
    {
        return await _context.Photocards
            .AsNoTracking()
            .Include(p => p.Album)
                .ThenInclude(a => a.Artist)
            .Include(p => p.Member)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByVersionAndAlbumIdAsync(string version, Guid albumId)
    {
        return await _context.Photocards
            .AnyAsync(p => p.Version == version && p.AlbumId == albumId);
    }

    public async Task<List<Photocard>> GetByArtistIdAsync(Guid artistId)
    {
        return await _context.Photocards
            .AsNoTracking()
            .Include(p => p.Album)
                .ThenInclude(a => a.Artist)
            .Include(p => p.Member)
            .Where(p => p.ArtistId == artistId)
            .ToListAsync();
    }

    public async Task<List<Photocard>> GetByMemberIdAsync(Guid memberId)
    {
        return await _context.Photocards
            .AsNoTracking()
            .Include(p => p.Album)
                .ThenInclude(a => a.Artist)
            .Include(p => p.Member)
            .Where(p => p.MemberId == memberId)
            .ToListAsync();
    }

}
