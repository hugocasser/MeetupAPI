using MediatR;
using Meetup.Application.Dtos;
using Meetup.Models;
using Meetup.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Meetup.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private readonly IEventDataClient _eventsDataClient;

    public EventsController(IEventDataClient eventDataClient)
    {
        _eventsDataClient = eventDataClient;
    }

    [SwaggerOperation(Summary = "Fetch events data from the events service")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<EventInformation>>> GetAllEvents()
    {
        var events = await _eventsDataClient.GetAllEvents();
        return Ok(events);
    }
    
    
    [SwaggerOperation(Summary = "Create events data in the events service")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CreateEventDto>>> CreateEvents([FromBody] CreateEventDto createEventDto)
    {
        var eventDto = await _eventsDataClient.CreateEvent(createEventDto);
        return Ok(eventDto);
    }
    
    [SwaggerOperation(Summary = "Update events data in the events service")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CreateEventDto>>> CreateEvents([FromBody] UpdateEventDto updateEventDto)
    {
        var eventDto = await _eventsDataClient.UpdateEvent(updateEventDto);
        return Ok(eventDto);
    }
    
    [SwaggerOperation(Summary = "Fetch events data by id from the events service")]
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<EventInformation>> GetEventById(DeleteEventDto eventDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        await _eventsDataClient.DeleteEvent(eventDto);
        return Ok();
    }
}