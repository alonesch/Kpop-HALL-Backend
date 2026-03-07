using KpopHall.Domain.Entities;
namespace KpopHall.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}