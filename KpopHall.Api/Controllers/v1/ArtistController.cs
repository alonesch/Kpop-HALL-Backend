using KpopHall.Application.Artists.CreateArtist;
using KpopHall.Application.Artists.ListArtists;
using KpopHall.Application.Artists.GetArtistById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KpopHall.Api.Controllers.v1;


[ApiController]
[Route("api/v1/artists")]

public class ArtistController : ControllerBase
{
    private readonly CreateArtistUseCase _createUseCase;
    private readonly ListArtistsUseCase _listUseCase;
    private readonly GetArtistByIdUseCase _getByIdUseCase;

    public ArtistController(CreateArtistUseCase useCase, ListArtistsUseCase listCase, GetArtistByIdUseCase getByIdUseCase)
    {
        _createUseCase = useCase;
        _listUseCase = listCase;
        _getByIdUseCase = getByIdUseCase;
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateArtistRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _listUseCase.ExecuteAsync();
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _getByIdUseCase.ExecuteAsync(id);
        return Ok(result);
    }
}
