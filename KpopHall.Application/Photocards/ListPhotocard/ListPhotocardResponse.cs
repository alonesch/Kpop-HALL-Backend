namespace KpopHall.Application.Photocards.ListPhotocard;

public class ListPhotocardResponse
{
    public Guid Id { get; set; }
    public string Version { get; set; } = null!;
    public Guid MemberId { get; set; }
    public bool IsIrregular { get; set; }
}