using KpopHall.Application.Interfaces;

namespace KpopHall.Application.Artists.ListArtists;

public class ListArtistsUseCase
{
    private readonly IArtistsRepository _repository;

    public ListArtistsUseCase(IArtistsRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ListArtistsResponse>> ExecuteAsync()
    { 
        var artists = await _repository.GetAllAsync();

        return artists.Select(a => new ListArtistsResponse
        {
            Id = a.Id,
            Name = a.Name
        }).ToList();
    }
        
}