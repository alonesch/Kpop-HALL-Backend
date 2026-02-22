using Microsoft.AspNetCore.Mvc;
using KpopHall.Application.Albums.CreateAlbum;
using KpopHall.Application.Albums.GetAlbum;
using KpopHall.Application.Albums.ListAlbums;
using Microsoft.AspNetCore.Authorization;

namespace KpopHall.Api.Controllers.v1;


[ApiController]
[Route("api/v1/artists/{artistId:int}/albums")]
public class  AlbumController : ControllerBase
{
    private readonly CreateAlbumUseCase _createUseCase;
    private readonly ListAlbumsUseCase _listAlbumsUseCase;
    private readonly GetAlbumByIdUseCase _getByIdUseCase;

    public AlbumController(CreateAlbumUseCase createUseCase, ListAlbumsUseCase listAlbumsUseCase, GetAlbumByIdUseCase getByIdUseCase)
    {
        _createUseCase = createUseCase;
        _listAlbumsUseCase = listAlbumsUseCase;
        _getByIdUseCase = getByIdUseCase;
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute]int artistId, [FromBody]CreateAlbumRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(artistId, request);

        return Created("api/v1/artists/{artistId}/albums/{response.Id}", response);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromRoute]int artistId)
    {
        var response = await _listAlbumsUseCase.ExecuteAsync(artistId);
        return Ok(response);
    }

    [HttpGet("{albumId:int}")]
    public async Task<IActionResult> GetById([FromRoute]int artistId, [FromRoute]int albumId)
    {
        var response = await _getByIdUseCase.ExecuteAsync(artistId, albumId);
        return Ok(response);
    }
}


