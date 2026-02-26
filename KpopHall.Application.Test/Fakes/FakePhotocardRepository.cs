//using KpopHall.Application.Interfaces;
//using KpopHall.Domain.Entities;

//namespace KpopHall.Application.Test.Fakes;

//public class FakePhotocardRepository : IPhotoCardsRepository
//{
//    private readonly List<Photocard> _photocards = new();
//    private int _IdCounter = 1;

//    public Task AddAsync(Photocard photocard)
//    {
//        typeof(Photocard).GetProperty("Id")!.SetValue(photocard, _IdCounter++);

//        _photocards.Add(photocard);

//        return Task.CompletedTask;
//    }

//    public Task<List<Photocard>> GetByAlbumIdAsync(int albumId)
//    {
//        var photocards = _photocards
//            .Where(p => p.AlbumId == albumId)
//            .ToList();

//        return Task.FromResult(photocards);
//    }

//    public Task<Photocard?> GetByIdAsync(int id)
//    {
//        var photocard = _photocards
//            .FirstOrDefault(p => p.Id == id);
//        return Task.FromResult(photocard);
//    }

//    public Task<bool> ExistsByNameAndAlbumIdAsync(string name, int albumId)
//    {
//        var exists = _photocards
//            .Any(p => p.Name == name && p.AlbumId == albumId);

//        return Task.FromResult(exists);

//    }
//}
