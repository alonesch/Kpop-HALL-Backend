//using KpopHall.Application.Interfaces;
//using KpopHall.Domain.Entities;

//namespace KpopHall.Application.Test.Fakes
//{
//    public class FakeAlbumRepository : IAlbumsRepository
//    {
//        private readonly List<Album> _albums = new();
//        private int _idCounter = 1;

//        public Task AddAsync(Album album)
//        {
//            typeof(Album).GetProperty("Id")!.SetValue(album, _idCounter++);

//            _albums.Add(album);

//            return Task.CompletedTask;
//        }

//        public Task<bool> ExistsByTitleAndArtistIdAsync(string title, int artistId)
//        {
//            var exists = _albums.Any(a =>
//                a.Title == title && a.ArtistId == artistId);

//            return Task.FromResult(exists);
//        }

//        public Task<List<Album>> GetByArtistIdAsync(int artistId)
//        {
//            var albums = _albums.
//                Where(a => a.ArtistId == artistId).
//                ToList();

//            return Task.FromResult(albums);
//        }

//        public Task<Album?> GetByIdAsync(int id)
//        {
//            var album = _albums.FirstOrDefault(a => a.Id == id);
//            return Task.FromResult(album);
//        }

//    }
//}