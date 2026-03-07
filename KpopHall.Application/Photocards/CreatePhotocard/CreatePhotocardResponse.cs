namespace KpopHall.Application.Photocards.CreatePhotocard;

public class  CreatePhotocardResponse
{
    public Guid Id { get; set; }
    public string Version { get; set; } = null!;
    public Guid AlbumId { get; set; }
    public Guid MemberId { get; set; }
    public bool IsIrregular { get; set; }
}