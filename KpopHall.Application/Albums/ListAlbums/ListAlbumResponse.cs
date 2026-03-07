namespace KpopHall.Application.Albums.ListAlbums;

public class ListAlbumsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
}
