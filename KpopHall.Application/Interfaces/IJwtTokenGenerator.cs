using KpopHall.Domain.Entities;

namespace KpopHall.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
