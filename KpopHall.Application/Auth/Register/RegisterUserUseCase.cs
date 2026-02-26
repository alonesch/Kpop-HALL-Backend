using KpopHall.Application.Interfaces;
using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;

namespace KpopHall.Application.Auth.Register;

public class RegisterUserUseCase
{
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserUseCase(
        IUsersRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
            throw new DomainException("Email already registered.");

        if (await _userRepository.ExistsByUsernameAsync(request.Username))
            throw new DomainException("Username already taken.");

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(
            request.Username,
            request.Email,
            passwordHash);

        await _userRepository.AddAsync(user);

        return new RegisterUserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}