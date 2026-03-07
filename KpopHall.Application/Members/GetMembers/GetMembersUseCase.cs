using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Members.GetMember;

public class GetMemberByIdUseCase
{
    private readonly IMembersRepository _repository;
    public GetMemberByIdUseCase(IMembersRepository repository)
    {
        _repository = repository;
    }
    public async Task<GetMemberResponse> ExecuteAsync(Guid id)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null)
            throw new DomainException("Member not found.");
        return new GetMemberResponse { Id = member.Id, Name = member.Name, ArtistId = member.ArtistId };
    }
}
