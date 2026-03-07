using KpopHall.Domain.Entities;
namespace KpopHall.Application.Interfaces;

public interface IMembersRepository
{
    Task AddAsync(Member member);
    Task<Member?> GetByIdAsync(Guid id);
    Task<List<Member>> GetAllAsync();
    Task<List<Member>> GetByArtistIdAsync(Guid artistId);
    Task<bool> ExistsByNameAndArtistIdAsync(string name, Guid artistId);
}