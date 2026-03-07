using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Users.GetMe;

public class GetMeUseCase
{
    private readonly IUsersRepository _usersRepository;
    private readonly ICurrentUserService _currentUserService;
    public GetMeUseCase(IUsersRepository usersRepository, ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }
    public async Task<GetMeResponse> ExecuteAsync(CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(_currentUserService.UserId, cancellationToken);
        if (user is null)
            throw new DomainException("User not found.");
        return new GetMeResponse(user.Id, user.Username, user.Email, user.Role, user.HasSeenTour);
    }
}