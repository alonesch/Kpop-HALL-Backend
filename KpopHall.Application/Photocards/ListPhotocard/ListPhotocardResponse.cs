namespace KpopHall.Application.Photocards.ListPhotocard;

public class ListPhotocardResponse
{
    public Guid Id { get; set; }
    public string Version { get; set; } = null!;
    public Guid MemberId { get; set; }
    public string MemberName { get; set; } = null!;
    public Guid ArtistId { get; set; }
    public string ArtistName { get; set; } = null!;
    public string AlbumTitle { get; set; } = null!;
    public bool IsIrregular { get; set; }
    public string? FrontsideImageUrl { get; set; }
}