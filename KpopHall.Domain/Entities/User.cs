using KpopHall.Domain.Exceptions;

namespace KpopHall.Domain.Entities;

public class User
{
    public int Id { get; private set; }

    public string Username { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public string Role { get; private set; } = "User";

    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAt { get; private set; }

    protected User() { }

    public User(string username, string email, string passwordHash, string role = "User")
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new DomainException("Username cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password hash cannot be empty.");

        Username = username.Trim();
        Email = email.Trim().ToLower();
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException("User already inactive.");

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            throw new DomainException("User already active.");

        IsActive = true;
    }

    public void ChangeRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new DomainException("Invalid role.");

        Role = role;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException("Invalid password.");

        PasswordHash = newPasswordHash;
    }
}