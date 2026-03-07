using KpopHall.Application.Users.GetMe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace KpopHall.Api.Controllers.v1;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(
        [FromServices] GetMeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(cancellationToken);
        return Ok(result);
    }
}