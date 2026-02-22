namespace KpopHall.Application.Albums.CreateAlbum;

public class CreateAlbumResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public int ArtistId { get; set; }

}

