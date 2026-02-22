//using KpopHall.Domain.Entities;
//using KpopHall.Domain.Exceptions;
//using Xunit;

//namespace KpopHall.Domain.Tests;

//public class PhotocardTests
//{
//    [Fact]
//    public void Posso_Criar_Photocard_Regular()
//    {
//        var name = "Card A";
//        var albumId = 1;

//        var photocard = new Photocard(name, albumId);

//        Assert.NotNull(photocard);
//        Assert.False(photocard.IsIrregular);
//    }

//    [Fact]
//    public void Posso_Criar_Photocard_Irregular()
//    {
//        var name = "Card B";
//        var albumId = 1;

//        var context = new DistributionContext("Ktown4u", "Korea", null, 100);

//        var photocard = new Photocard(name, albumId, context);

//        Assert.True(photocard.IsIrregular);
//    }

//    [Fact]
//    public void Não_Posso_Criar_Photocard_Com_Nome_Vazio()
//    {
//        Assert.Throws<DomainException>(() =>
//            new Photocard("", 1));
//    }

//    [Fact]
//    public void Não_Posso_Criar_Photocard_Com_AlbumId_Invalido()
//    {
//        Assert.Throws<DomainException>(() =>
//            new Photocard("Card C", 0));
//    }

//}