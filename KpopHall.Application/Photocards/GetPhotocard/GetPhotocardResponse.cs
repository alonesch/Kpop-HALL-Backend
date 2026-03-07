namespace KpopHall.Application.Photocards.GetPhotocard;

public class GetPhotocardResponse
{
    public Guid Id { get; set; }
    public string Version { get; set; } = null!;
    public Guid AlbumId { get; set; }
    public Guid MemberId { get; set; }
    public bool IsIrregular { get; set; }
    public string? Store { get; set; }
    public string? Region { get; set; }
    public string? Event { get; set; }
    public int? PrintQuantity { get; set; }
}