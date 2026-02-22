namespace KpopHall.Application.Photocards.CreatePhotocard;

public class  CreatePhotocardResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AlbumId { get; set; }
    public bool IsIrregular { get; set; }
}