using KpopHall.Application.Artists.CreateArtist;
using KpopHall.Application.Test.Fakes;
using KpopHall.Domain.Exceptions;
using System.Runtime.CompilerServices;
using Xunit;

namespace KpopHall.Application.Test;

public class CreateArtistsUseCaseTests
{
    [Fact]
    public async Task CreateArtist_Deve_Retornar_Artist_Criado()
    {
        //Arrange
        var repository = new FakeArtistRepository();
        var useCase = new CreateArtistUseCase(repository);

        var request = new CreateArtistRequest
        {
            Name = "Stray Kids"
        };

        //Act
        var response = await useCase.ExecuteAsync(request);

        //Assert
        Assert.Equal(1, response.Id);
        Assert.Equal("Stray Kids", response.Name);
    }

    [Fact]
    public async Task Não_Deve_Criar_Artista_Com_Nome_Duplicado()
    {
        //Arrange
        var repository = new FakeArtistRepository();
        var useCase = new CreateArtistUseCase(repository);
        
        var request = new CreateArtistRequest
        {
            Name = "Stray Kids"
        };

        //Act and Assert
        await useCase.ExecuteAsync(request);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.ExecuteAsync(request));
    }

}