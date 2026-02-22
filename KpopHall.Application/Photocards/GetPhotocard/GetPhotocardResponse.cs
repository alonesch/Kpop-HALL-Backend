namespace KpopHall.Application.Photocards.GetPhotocard;

public class GetPhotocardResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AlbumId { get; set; }
    public bool IsIrregular { get; set; }

    public string? Store { get; set; }
    public string? Region { get; set; }
    public string? Event { get; set; }
    public int? PrintQuantity { get; set; }
}