using System.Reflection.Metadata;

namespace KpopHall.Application.Auth.Register;

public class RegisterUserResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;

}
