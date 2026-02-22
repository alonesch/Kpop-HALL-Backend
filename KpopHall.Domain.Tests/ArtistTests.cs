using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
using Xunit;

namespace KpopHall.Domain.Tests;

public class ArtistTests
{
    [Fact]
    public void Should_Create_Artist_With_Valid_Name()
    {
        //Arrange
        var name = ("s");

        //Act & Assert
        Assert.Throws<DomainException>(() => new Artist(name));
    }
}