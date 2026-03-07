namespace KpopHall.Application.Users.GetMe;

public record GetMeResponse(Guid Id, string Username, string Email, string Role, bool HasSeenTour);