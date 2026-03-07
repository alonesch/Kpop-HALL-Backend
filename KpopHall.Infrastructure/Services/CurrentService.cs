using KpopHall.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace KpopHall.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public Guid UserId
    {
        get
        {
            var userIdClaim = _contextAccessor
                .HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid token.");
            return userId;
        }
    }
}