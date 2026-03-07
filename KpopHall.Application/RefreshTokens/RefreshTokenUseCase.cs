using KpopHall.Application.Interfaces;
using KpopHall.Application.RefreshTokens;
using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Auth.RefreshUser;

public class RefreshUserUseCase
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RefreshUserUseCase(IRefreshTokenRepository refreshTokenRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<RefreshUserResponse> ExecuteAsync(RefreshUserRequest request, CancellationToken cancellationToken)
    {
        var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        if (existingToken is null)
            throw new DomainException("Invalid refresh token.");
        if (existingToken.IsRevoked)
            throw new DomainException("Refresh token revoked.");
        if (existingToken.IsExpired())
            throw new DomainException("Refresh token expired.");
        existingToken.Revoke();
        var newAccessToken = _jwtTokenGenerator.GenerateToken(existingToken.User);
        var newRefreshToken = new RefreshToken(
            existingToken.User,
            Guid.NewGuid().ToString(),
            DateTime.UtcNow.AddDays(7)
        );
        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
        return new RefreshUserResponse(newAccessToken, newRefreshToken.Token);
    }
}