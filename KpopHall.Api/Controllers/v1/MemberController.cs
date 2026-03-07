using KpopHall.Application.Members.CreateMember;
using KpopHall.Application.Members.GetMember;
using KpopHall.Application.Members.ListMember;
using Microsoft.AspNetCore.Mvc;

namespace KpopHall.Api.Controllers.v1;

[ApiController]
[Route("api/v1/")]
public class MemberController : ControllerBase
{
    private readonly CreateMemberUseCase _createUseCase;
    private readonly GetMemberByIdUseCase _getUseCase;
    private readonly ListMemberUseCase _listUseCase;

    public MemberController(CreateMemberUseCase createUseCase, GetMemberByIdUseCase getUseCase, ListMemberUseCase listUseCase)
    {
        _createUseCase = createUseCase;
        _getUseCase = getUseCase;
        _listUseCase = listUseCase;
    }

    
    [HttpPost("artists/{artistId:Guid}/members")]
    public async Task<IActionResult> Create([FromRoute] Guid artistId, [FromBody] CreateMemberRequest request)
    {
        var response = await _createUseCase.ExecuteAsync(artistId, request);
        return Created($"/api/v1/artists/{artistId}/members/{response.Id}", response);
    }

    
    [HttpGet("members")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _listUseCase.ExecuteAsync();
        return Ok(result);
    }


    [HttpGet("artists/{artistId:Guid}/members")]
    public async Task<IActionResult> GetByArtist([FromRoute] Guid artistId)
    {
        var result = await _listUseCase.ExecuteByArtistAsync(artistId);
        return Ok(result);
    }

    
    [HttpGet("members/{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _getUseCase.ExecuteAsync(id);
        return Ok(result);
    }
}