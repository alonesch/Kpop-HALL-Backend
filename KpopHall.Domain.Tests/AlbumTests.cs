using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
using Xunit;


namespace KpopHall.Domain.Tests;

public class AlbumTests
{
    [Fact]
    public void Não_Deve_Criar_Album_Com_artistId_Invalido()
    {
        //Arrange
        var title = "KKKKKK";
        var year = 2000;
        var artistId = 0;
        //Act & Assert
        Assert.Throws<DomainException>(() => new Album(title, year, artistId));

    }
    [Fact]
    public void Não_Deve_Criar_Album_Com_title_Invalido()
    {
        //Arrange
        var title = "";
        var year = 2000;
        var artistId = 1;
        //Act & Assert
        Assert.Throws<DomainException>(() => new Album(title, year, artistId));
    }
    [Fact]
    public void Não_Deve_Criar_Album_Com_Ano_Invalido()
    {
        //Arrange
        var title = "teste";
        var year = 0;
        var artistId = 1;

        //Act & Assert
        Assert.Throws<DomainException>(() => new Album(title, year, artistId));
    }

    [Fact]
    public void Criando_Album_Valido()
    {
        //Arrange
        var title = "STAY";
        var year = 2018;
        var artistId = 1;

        //Act
        var album = new Album(title, year, artistId);

        //Assert
        Assert.Equal(title, album.Title);
        Assert.Equal(year, album.Year);
        Assert.Equal(artistId, album.ArtistId);
    }

}
