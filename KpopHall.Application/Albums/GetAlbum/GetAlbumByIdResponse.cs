namespace KpopHall.Application.Albums.GetAlbum;

public class GetAlbumByIdResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public Guid ArtistId { get; set; }
}