namespace KpopHall.Application.Albums.GetAlbum;

public class GetAlbumByIdResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public int ArtistId { get; set; }
}