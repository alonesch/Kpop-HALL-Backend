using KpopHall.Application.Photocards.GetPhotocard;
using KpopHall.Application.Photocards.ListPhotocard;
using KpopHall.Application.Photocards.CreatePhotocard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KpopHall.Api.Controllers.v1;


[ApiController]
[Route("api/v1/artists/{artistId:int}/album/{albumId:int}/photocards")]
public class PhotocardController : ControllerBase
{
    private readonly GetPhotocardUseCase _getUseCase;
    private readonly ListPhotocardUseCase _listUseCase;
    private readonly CreatePhotocardUseCase _createUseCase;
    public PhotocardController(GetPhotocardUseCase getUseCase, ListPhotocardUseCase listUseCase, CreatePhotocardUseCase createUseCase)
    {
        _getUseCase = getUseCase;
        _listUseCase = listUseCase;
        _createUseCase = createUseCase;
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(int artistId, int albumId, CreatePhotocardRequest request)
    {
         var response = await _createUseCase.ExecuteAsync(artistId, albumId, request);

        return Created($"/api/v1/artists/{artistId}/album/{albumId}/photocards/{response.Id}", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int artistId, int albumId)
    {
        var result = await _listUseCase.ExecuteAsync(artistId, albumId);
        
        return Ok(result);
    }

    [HttpGet("{photocardId:int}")]
    public async Task<IActionResult> GetById(int artistId, int albumId, int photocardId)
    {
        var result = await _getUseCase.ExecuteAsync(artistId, albumId, photocardId);
        
        return Ok(result);
    }
}
