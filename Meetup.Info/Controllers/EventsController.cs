using MediatR;
using Meetup.Info.Application.Commands.GetEventById;
using Meetup.Info.Application.Commands.GetEventByTitleAndDate;
using Meetup.Info.Application.Commands.GetEvents;
using Meetup.Info.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.Info.Controllers;

[Route("api/[controller]")]
[SwaggerTag("Events controller")]
[ApiController]
public class EventsController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [SwaggerOperation("Get all Events")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDetailsDTO>>> GetAllEvents()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetEventsQuery()));
    }
    
    [SwaggerOperation("Get event by title and date")]
    [HttpGet("{title}/{date}")]
    public async Task<ActionResult<EventDetailsDTO>> GetEventsByTitleAndDate(string? title, DateOnly date)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetEventByTitleAndDateQuery
        {
            Title = title,
            EventDate = date
        }));
    }
    [SwaggerOperation("Get event by title and date")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EventDetailsDTO>> GetEventsById(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetEventByIdQuery
        {
            Id = id,
        }));
    }
}