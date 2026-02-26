using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Auth.Login;

public class LoginUseCase
{
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUseCase(
        IUsersRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> ExecuteAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null || !user.IsActive)
            throw new DomainException("Invalid credentials.");

        if (!_passwordHasher.Verify(password, user.PasswordHash))
            throw new DomainException("Invalid credentials.");

        return _tokenGenerator.GenerateToken(user);
    }
}