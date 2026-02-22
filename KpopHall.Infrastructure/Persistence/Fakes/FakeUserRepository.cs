using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;

namespace KpopHall.Infrastructure.Persistence.Fakes;

public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private int _idCounter = 1;

    public Task AddAsync(User user)
    {
        typeof(User)
            .GetProperty("Id")!
            .SetValue(user, _idCounter++);

        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email == email.ToLower());
        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        return Task.FromResult(
            _users.Any(u => u.Email == email.ToLower()));
    }

    public Task<bool> ExistsByUsernameAsync(string username)
    {
        return Task.FromResult(
            _users.Any(u => u.Username == username));
    }
}