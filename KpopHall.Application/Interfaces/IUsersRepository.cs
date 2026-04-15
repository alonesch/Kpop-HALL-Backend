using KpopHall.Domain.Entities;
namespace KpopHall.Application.Interfaces;

public interface IUsersRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<User?> GetByEmailOrUsernameAsync(string email, string username);
}