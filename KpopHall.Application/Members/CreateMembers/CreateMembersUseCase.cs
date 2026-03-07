using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
using KpopHall.Domain.Entities;
namespace KpopHall.Application.Members.CreateMember;

public class CreateMemberUseCase
{
    private readonly IMembersRepository _repository;
    private readonly IArtistsRepository _artistRepository;
    public CreateMemberUseCase(IMembersRepository repository, IArtistsRepository artistRepository)
    {
        _repository = repository;
        _artistRepository = artistRepository;
    }
    public async Task<CreateMemberResponse> ExecuteAsync(Guid artistId, CreateMemberRequest request)
    {
        var artist = await _artistRepository.GetByIdAsync(artistId);
        if (artist == null)
            throw new DomainException("Artist not found.");
        var exists = await _repository.ExistsByNameAndArtistIdAsync(request.Name, artistId);
        if (exists)
            throw new DomainException("Member with this name already exists for this artist.");
        var member = new Member(Guid.NewGuid(), request.Name, artistId);
        await _repository.AddAsync(member);
        return new CreateMemberResponse { Id = member.Id, Name = member.Name, ArtistId = member.ArtistId };
    }
}