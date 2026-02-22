using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;

namespace KpopHall.Infrastructure.Persistence.Fakes;

public class FakeArtistRepository : IArtistRepository
{
    private readonly List<Artist> _artist = new();
    private int _idCounter = 1;

    public Task AddAsync(Artist artist)
    {
        typeof(Artist)
            .GetProperty("Id")!
            .SetValue(artist, _idCounter++);

        _artist.Add(artist);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        var exists = _artist.Any(a => a.Name == name);
        return Task.FromResult(exists);
    }

    public Task<List<Artist>> GetAllAsync()
    {
        return Task.FromResult(_artist);
    }

    public Task<Artist?> GetByIdAsync(int id)
    {
        var artist = _artist.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(artist);

    }

}