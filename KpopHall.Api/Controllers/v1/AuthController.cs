using KpopHall.Application.Auth.Login;
using KpopHall.Application.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace KpopHall.Api.Controllers.v1;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserUseCase _registerUseCase;
    private readonly LoginUseCase _loginUseCase;

    public AuthController(
        RegisterUserUseCase registerUseCase,
        LoginUseCase loginUseCase)
    {
        _registerUseCase = registerUseCase;
        _loginUseCase = loginUseCase;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _registerUseCase.ExecuteAsync(request);
        return Created("", result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _loginUseCase.ExecuteAsync(
            request.Email,
            request.Password);

        return Ok(new { token });
    }
}