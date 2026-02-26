//using KpopHall.Application.Photocards.CreatePhotocard;
//using KpopHall.Application.Test.Fakes;
//using KpopHall.Domain.Entities;
//using KpopHall.Domain.Exceptions;
//using Xunit;

//namespace KpopHall.Application.Test;

//public class CreatePhotocardUseCaseTests
//{
//    [Fact]
//    public async Task Deve_Criar_Photocard_Regular()
//    {
//        var artistRepo = new FakeArtistRepository();
//        var albumRepo = new FakeAlbumRepository();
//        var photocardRepo = new FakePhotocardRepository();

//        var artist = new Artist("Stray Kids");
//        await artistRepo.AddAsync(artist);

//        var album = new Album("5-STAR", 2023, artist.Id);
//        await albumRepo.AddAsync(album);

//        var useCase = new CreatePhotocardUseCase(
//            photocardRepo,
//            albumRepo,
//            artistRepo);

//        var request = new CreatePhotocardRequest
//        {
//            Name = "Han - Version A"
//        };

//        var result = await useCase.ExecuteAsync(
//            artist.Id,
//            album.Id,
//            request);

//        Assert.NotNull(result);
//        Assert.False(result.IsIrregular);
//    }

//    [Fact]
//    public async Task Não_Deve_Criar_Se_Artista_Não_Existir()
//    {
//        var artistaRepo = new FakeArtistRepository();
//        var albumRepo = new FakeAlbumRepository();
//        var photocardRepo = new FakePhotocardRepository();

//        var useCase = new CreatePhotocardUseCase(
//            photocardRepo,
//            albumRepo,
//            artistaRepo);
//        var request = new CreatePhotocardRequest
//        {
//            Name = "Teste"
//        };

//        await Assert.ThrowsAsync<DomainException>(async () =>
//            await useCase.ExecuteAsync(1, 1, request));
//    }

//    [Fact]
//    public async Task Não_Criar_Duplicata()
//    {
//        var artistRepo = new FakeArtistRepository();
//        var albumRepo = new FakeAlbumRepository();
//        var photocardRepo = new FakePhotocardRepository();
//        var artist = new Artist("Stray Kids");
//        await artistRepo.AddAsync(artist);
//        var album = new Album("5-STAR", 2023, artist.Id);
//        await albumRepo.AddAsync(album);
//        var useCase = new CreatePhotocardUseCase(
//            photocardRepo,
//            albumRepo,
//            artistRepo);
//        var request = new CreatePhotocardRequest
//        {
//            Name = "Han - Version A"
//        };
//        await useCase.ExecuteAsync(
//            artist.Id,
//            album.Id,
//            request);
//        await Assert.ThrowsAsync<DomainException>(async () =>
//            await useCase.ExecuteAsync(
//                artist.Id,
//                album.Id,
//                request));
//    }

//    [Fact]
//    public async Task Pode_Criar_Mesmo_Nome_Album_Diferente()
//    {
//        var artistRepo = new FakeArtistRepository();
//        var albumRepo = new FakeAlbumRepository();
//        var photocardRepo = new FakePhotocardRepository();

//        var artist = new Artist("Stray Kids");
//        await artistRepo.AddAsync(artist);

//        var album1 = new Album("KARMA", 2024, artist.Id);
//        await albumRepo.AddAsync(album1);

//        var album2 = new Album("5-STAR", 2023, artist.Id);
//        await albumRepo.AddAsync(album2);

//        var useCase = new CreatePhotocardUseCase(
//            photocardRepo,
//            albumRepo,
//            artistRepo);

//        var request = new CreatePhotocardRequest
//        {
//            Name = "Han - Version A"
//        };

//        await useCase.ExecuteAsync(
//            artist.Id,
//            album1.Id,
//            request);
//        await useCase.ExecuteAsync(
//            artist.Id,
//            album2.Id,
//            request);
//    }
//}
