using KpopHall.Application.Photocards.GetPhotocard;
using KpopHall.Application.Photocards.ListPhotocard;
using KpopHall.Application.Photocards.CreatePhotocard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KpopHall.Api.Controllers.v1;


[ApiController]
[Route("api/v1/")]
public class PhotocardController : ControllerBase
{
    private readonly ListPhotocardUseCase _listUseCase;
    private readonly CreatePhotocardUseCase _createUseCase;
    private readonly GetPhotocardUseCase _getUseCase;
    public PhotocardController(ListPhotocardUseCase listUseCase, CreatePhotocardUseCase createUseCase, GetPhotocardUseCase getUseCase)
    {
        _listUseCase = listUseCase;
        _createUseCase = createUseCase;
        _getUseCase = getUseCase;
    }

    
    [HttpPost("artists/{artistId:Guid}/albums/{albumId:Guid}/photocards")]
    public async Task<IActionResult> Create([FromRoute] Guid artistId, [FromRoute] Guid albumId, [FromBody] CreatePhotocardRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(artistId, albumId, request);
        return Created($"/api/v1/artists/{artistId}/albums/{albumId}/photocards/{response.Id}", response);
    }

    
    [HttpGet("photocards")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _listUseCase.ExecuteAsync();
        return Ok(result);
    }

    
    [HttpGet("albums/{albumId:Guid}/photocards")]
    public async Task<IActionResult> GetByAlbum([FromRoute] Guid albumId)
    {
        var result = await _listUseCase.ExecuteByAlbumAsync(albumId);
        return Ok(result);
    }

    
    [HttpGet("artists/{artistId:Guid}/photocards")]
    public async Task<IActionResult> GetByArtist([FromRoute] Guid artistId)
    {
        var result = await _listUseCase.ExecuteByArtistAsync(artistId);
        return Ok(result);
    }

    
        [HttpGet("photocards/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _getUseCase.ExecuteAsync(id);
            return Ok(result);
        }
        

    [HttpGet("members/{memberId:Guid}/photocards")]
    public async Task<IActionResult> GetByMember([FromRoute] Guid memberId)
    {
        var result = await _listUseCase.ExecuteByMemberAsync(memberId);
        return Ok(result);
    }
}