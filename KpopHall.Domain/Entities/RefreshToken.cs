namespace KpopHall.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    private RefreshToken() { }
    public RefreshToken(User user, string token, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        UserId = user.Id;
        Token = token;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }
    public void Revoke() => IsRevoked = true;
    public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;
}