using Meetup.Application.Dtos;
using Meetup.Models;
using Meetup.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpeakersController : ControllerBase
{
    private readonly ISpeakerDataClient _authorsDataClient;

    public SpeakersController(ISpeakerDataClient authorsDataClient)
    {
        _authorsDataClient = authorsDataClient;
    }

    [SwaggerOperation(Summary = "Fetch speakers data from the speaker service")]
    [HttpGet] 
    [Authorize]
    public async Task<ActionResult<IEnumerable<Speaker>>> GetAllSpeakers()
    {
        var speakers = await _authorsDataClient.GetAllSpeakers();
        return Ok(speakers);
    }
    
    [SwaggerOperation(Summary = "Create speaker in the speakers service")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Speaker>>> CreateSpeaker([FromBody] CreateSpeakerDto speakersDto)
    {
        var speakers = await _authorsDataClient.CreateSpeaker(speakersDto);
        return Ok(speakers);
    }
}