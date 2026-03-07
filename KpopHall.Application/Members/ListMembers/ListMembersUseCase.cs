using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Members.ListMember;

public class ListMemberUseCase
{
    private readonly IMembersRepository _repository;
    private readonly IArtistsRepository _artistRepository;
    public ListMemberUseCase(IMembersRepository repository, IArtistsRepository artistRepository)
    {
        _repository = repository;
        _artistRepository = artistRepository;
    }
    public async Task<List<ListMemberResponse>> ExecuteAsync()
    {
        var members = await _repository.GetAllAsync();
        return members.Select(Map).ToList();
    }
    public async Task<List<ListMemberResponse>> ExecuteByArtistAsync(Guid artistId)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");
        var members = await _repository.GetByArtistIdAsync(artistId);
        return members.Select(Map).ToList();
    }
    private static ListMemberResponse Map(KpopHall.Domain.Entities.Member m) => new()
    {
        Id = m.Id,
        Name = m.Name,
        ArtistId = m.ArtistId
    };
}