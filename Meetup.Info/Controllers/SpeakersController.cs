using MediatR;
using Meetup.Info.Application.Commands.GetSpeakers;
using Meetup.Info.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.Info.Controllers;

[Route("api/[controller]")]
[SwaggerTag("Authors controller")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    
    public AuthorsController(ILogger<AuthorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpeakerDetailsDTO>>> GetAuthors()
    {
        _logger.LogInformation("Getting speakers from event-service");
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        return Ok(await Mediator.Send(new GetSpeakersQuery()));
    }
}