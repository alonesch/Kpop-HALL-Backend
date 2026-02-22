//using KpopHall.Application.Interfaces;
//using KpopHall.Domain.Entities;
//using KpopHall.Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;

//namespace KpopHall.Infrastructure.Repositories;

//public class ArtistsRepository : IArtistRepository
//{
//    private readonly KpopHallDbContext _context;

//    public ArtistsRepository(KpopHallDbContext context)
//    {
//        _context = context;
//    }

//    public async Task AddAsync(Artist artist)
//    {
//        await _context.Artists.AddAsync(artist);
//        await _context.SaveChangesAsync();
//    }

//    public async Task<bool> ExistsByNameAsync(string name)
//    {
//        return await _context.Artists
//            .AnyAsync(a => a.Name == name);
//    }
//}