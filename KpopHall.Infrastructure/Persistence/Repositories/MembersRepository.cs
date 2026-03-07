using KpopHall.Domain.Entities;
using KpopHall.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace KpopHall.Infrastructure.Persistence.Repositories;

public class MembersRepository : IMembersRepository
{
    private readonly KpopHallDbContext _context;
    public MembersRepository(KpopHallDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
    }
    public async Task<Member?> GetByIdAsync(Guid id)
    {
        return await _context.Members
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<List<Member>> GetAllAsync()
    {
        return await _context.Members
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<List<Member>> GetByArtistIdAsync(Guid artistId)
    {
        return await _context.Members
            .AsNoTracking()
            .Where(m => m.ArtistId == artistId)
            .ToListAsync();
    }
    public async Task<bool> ExistsByNameAndArtistIdAsync(string name, Guid artistId)
    {
        return await _context.Members
            .AnyAsync(m => m.Name == name && m.ArtistId == artistId);
    }
}